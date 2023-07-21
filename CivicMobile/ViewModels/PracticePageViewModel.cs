using CivicMobile.Database;
using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CivicMobile.ViewModels;

public partial class PracticePageViewModel : BaseViewModel
{
    public ObservableCollection<Question> ExamQuestions { get; set; } = new();

    [ObservableProperty]
    public bool _isQuestionAnswered;

    [ObservableProperty]
    private Question _currentQuestion;

    [ObservableProperty]
    private Answer _selectedAnswer;

    private QuestionService questionService;
    private readonly IAudioManager audioManager;
    private readonly CivicDbContext dbContext;

    [ObservableProperty]
    private bool _canGoNextQuestion = false;

    [ObservableProperty]
    private SelectionMode _selectionMode = SelectionMode.Single;

    public PracticePageViewModel(QuestionService questionService, IAudioManager audioManager, CivicDbContext dbContext)
    {
        Title = "Practice Exam";
        this.questionService = questionService;
        this.audioManager = audioManager;
        this.dbContext = dbContext;
    }

    [RelayCommand]
    private void Refresh()
    {
        CurrentQuestion = ExamQuestions.FirstOrDefault();
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
    private async void NextQuestion()
    {
        SelectionMode = SelectionMode.Single;
        var currentIndex = CurrentQuestion.QuestionNumber;
        var next = ++currentIndex;
        var nextQuestion = ExamQuestions.FirstOrDefault(item => item.QuestionNumber == next);
        // check to show the practice exam result

        CurrentQuestion = nextQuestion;
        IsQuestionAnswered = false;
        CanGoNextQuestion = false;
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
            // update the user record
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
}