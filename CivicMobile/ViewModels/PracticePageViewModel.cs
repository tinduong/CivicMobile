using CivicMobile.Interfaces;
using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.SimpleAudioPlayer;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace CivicMobile.ViewModels
{
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
        private IAudioPlayer audioPlayer;

        [ObservableProperty]
        private Boolean _isDone = false;

        [ObservableProperty]
        private SelectionMode _selectionMode = SelectionMode.Single;

        public PracticePageViewModel(QuestionService questionService, IAudioPlayer audioPlayer)
        {
            Title = "Practice Exam";
            this.questionService = questionService;
            this.audioPlayer = audioPlayer;
        }

        [RelayCommand]
        private async Task Refresh()
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
                await Shell.Current.DisplayAlert("Error", "Loaded with error", "close");
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
            var nextQuestion = ExamQuestions.FirstOrDefault(item => item.QuestionNumber == next)?? ExamQuestions.FirstOrDefault();


            CurrentQuestion = nextQuestion;
            IsQuestionAnswered = false;
        }

        private CancellationTokenSource cts;

        [RelayCommand]
        private async Task AnswerSelected()
        {
           
            if (CurrentQuestion == null || IsQuestionAnswered) return;
            IsQuestionAnswered = true;

            #region
            var isCorrect = CurrentQuestion.Answers.Where(a => a.IsCorrect).Contains(SelectedAnswer);

            if (isCorrect)
            {
                audioPlayer.Play("correct.wav");
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
}