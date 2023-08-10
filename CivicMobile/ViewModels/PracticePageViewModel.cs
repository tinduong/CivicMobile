using CivicMobile.Database;
using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CivicMobile.ViewModels;

public partial class PracticePageViewModel : BaseViewModel, IDisposable
{
    public ObservableCollection<Question> ExamQuestions { get; set; } = new();

    [ObservableProperty]
    public bool _isQuestionAnswered;

    [ObservableProperty]
    private Question _currentQuestion;

    [ObservableProperty]
    private Answer _selectedAnswer;

    private CancellationTokenSource _cts;
    private readonly QuestionService questionService;
    private readonly IAudioManager audioManager;
    private readonly TextToSpeechService speakService;
    private readonly CivicDbContext dbContext;

    [ObservableProperty]
    private bool _canGoNextQuestion = false;

    [ObservableProperty]
    private SelectionMode _selectionMode = SelectionMode.Single;

    public PracticePageViewModel(QuestionService questionService, IAudioManager audioManager, TextToSpeechService speakService, CivicDbContext dbContext)
    {
        Title = "Practice Exam";
        _cts = new CancellationTokenSource();
        this.questionService = questionService;
        this.audioManager = audioManager;
        this.speakService = speakService;
        this.dbContext = dbContext;
    }

    [RelayCommand]
    private void Refresh()
    {
        CurrentQuestion = ExamQuestions.FirstOrDefault();
    }

    [RelayCommand]
    public void OnPageClosedCleanup()
    {
        // clean up here
        CurrentQuestion = null;
    }

    [RelayCommand]
    public async Task LoadExamQuestions()
    {
        try
        {
            IsBusy = true;
            var questions = await questionService.GetExamQuestions();

            if (ExamQuestions.Any())
                ExamQuestions.Clear();

            foreach (var question in questions)
            {
                ExamQuestions.Add(question);
            }

            CurrentQuestion = ExamQuestions.FirstOrDefault();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", $"Loaded with error {e}", "close");
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void NextQuestion()
    {
        SelectionMode = SelectionMode.Single;
        var currentIndex = CurrentQuestion is not null ? CurrentQuestion.QuestionNumber : 0;
        var next = ++currentIndex;
        var nextQuestion = ExamQuestions.FirstOrDefault(item => item.QuestionNumber == next);

        CurrentQuestion = nextQuestion;
        IsQuestionAnswered = false;
        CanGoNextQuestion = false;

        // read the question
        _cts.Cancel();
        _cts = null;
        _cts = new CancellationTokenSource();
    }

    [RelayCommand]
    public async void ReadQuestion()
    {
        await Task.Run(async () =>
        {
            await speakService.Speak(CurrentQuestion.QuestionDescription, "en-US", _cts);
        });
    }

    private async Task<UserRecord> GetUserRecord()
    {
        var userRecord = (await dbContext.Get<UserRecord>()).ToList().FirstOrDefault(item => item.QuestionNumber == CurrentQuestion.QuestionNumber);

        if (userRecord is not null)
        {
            return userRecord;
        }

        var addedUserRecord = new UserRecord
        {
            QuestionNumber = CurrentQuestion.QuestionNumber
        };
        await dbContext.Add(addedUserRecord);
        return addedUserRecord;
    }

    [RelayCommand]
    private async Task AnswerSelected()
    {
        if (CurrentQuestion == null || IsQuestionAnswered) return;
        IsQuestionAnswered = true;

        var userRecord = await GetUserRecord();

        #region
        var isCorrect = CurrentQuestion.Answers.Where(a => a.IsCorrect).Contains(SelectedAnswer);

        if (isCorrect)
        {
            userRecord.CorrectCount++;

            // todo this probably need to be moved out of here
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("CivicMobile.Audio." + "correct.wav");

            var player = audioManager.CreatePlayer(stream);
            player.Play();
        }
        else
        {
            userRecord.WrongCount++;
        }

        await dbContext.Update(userRecord);
        var check = await dbContext.Get<UserRecord>();

        #endregion
        foreach (var answer in CurrentQuestion.Answers)
        {
            answer.IsQuestionAnswered = true;
            answer.IsSelectedAnswer = answer == SelectedAnswer;
        }

        var cachCurrentQuestion = CurrentQuestion;
        CurrentQuestion = null;
        SelectionMode = SelectionMode.None;
        CurrentQuestion = cachCurrentQuestion;
        CanGoNextQuestion = true;
    }

    public void Dispose()
    {
        if (_cts != null)
        {
            _cts.Dispose();
            _cts = null;
        }
    }
}