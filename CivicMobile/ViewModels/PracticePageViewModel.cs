using CivicMobile.Interfaces;
using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
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

    [ObservableProperty]
    private string _subTitle = "Yeah";

    private QuestionService questionService;
    private readonly IAudioManager audioManager;

    //private readonly IAudioService audioService;

    [ObservableProperty]
    private Boolean _isDone = false;

    [ObservableProperty]
    private SelectionMode _selectionMode = SelectionMode.Single;

    public PracticePageViewModel(QuestionService questionService, IAudioManager audioManager)
    {
        Title = "Practice Exam";
        this.questionService = questionService;
        this.audioManager = audioManager;
    }

    [RelayCommand]
    private void Refresh()
    {
        CurrentQuestion = ExamQuestions.FirstOrDefault();
    }

    [RelayCommand]
    private async Task LoadExamQuestions()
    {
        try
        {
            IsBusy = true;
            if (IsDone) return;
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
            IsDone = true;
        }
    }

    [RelayCommand]
    private void NextQuestion()
    {
        SelectionMode = SelectionMode.Single;
        var currentIndex = CurrentQuestion.QuestionNumber;
        var next = ++currentIndex;
        var nextQuestion = ExamQuestions.FirstOrDefault(item => item.QuestionNumber == next) ?? ExamQuestions.FirstOrDefault();

        CurrentQuestion = nextQuestion;
        IsQuestionAnswered = false;
    }

    [RelayCommand]
    private async void AnswerSelected()
    {
        if (CurrentQuestion == null || IsQuestionAnswered) return;
        IsQuestionAnswered = true;

        #region
        var isCorrect = CurrentQuestion.Answers.Where(a => a.IsCorrect).Contains(SelectedAnswer);

        if (isCorrect)
        {
            // todo this probably need to be moved out of here
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("CivicMobile.Audio." + "correct.wav");

            var player = audioManager.CreatePlayer(stream);
            player.Play();
        }

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
    }
}