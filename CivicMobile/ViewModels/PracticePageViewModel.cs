using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CivicMobile.ViewModels
{
    public partial class PracticePageViewModel : BaseViewModel
    {
        public ObservableCollection<Question> ExamQuestions { get; set; } = new();
        public bool IsQuestionAnswered { get; private set; }

        [ObservableProperty]
        private Question _currentQuestion;

        [ObservableProperty]
        private Answer _selectedAnswer;

        [ObservableProperty]
        private string _subTitle = "Yeah";

        private QuestionService questionService;

        [ObservableProperty]
        private Boolean _isDone = false;

        public PracticePageViewModel(QuestionService questionService)
        {
            Title = "Practice Exam";
            this.questionService = questionService;
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
            var currentIndex = CurrentQuestion.QuestionNumber;
            var next = ++currentIndex;
            CurrentQuestion = ExamQuestions.FirstOrDefault(item => item.QuestionNumber == next);
        }

        [RelayCommand]
        private async Task AnswerSelected()
        {
            if (CurrentQuestion == null) return;

            var test = SelectedAnswer;

            var isCorrect = CurrentQuestion.Answers.Where(a => a.IsCorrect).Contains(SelectedAnswer);

            if (isCorrect)
            {
                //_audioService.Play("correct.wav");
            }
            else
            {
                var correctAnswer = CurrentQuestion.Answers.Where(item => item.IsCorrect).FirstOrDefault().AnswerText;
            }

            foreach (var answer in CurrentQuestion.Answers)
            {
                answer.IsQuestionAnswered = true;
                answer.IsSelectedAnswer = answer == SelectedAnswer;
            }

            var cachCurrentQuestion = CurrentQuestion;
            CurrentQuestion = null;
            CurrentQuestion = cachCurrentQuestion;
        }
    }
}