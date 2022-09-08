using CivicMobile.Models;
using CivicMobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CivicMobile.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private QuestionService questionService;

        public ObservableCollection<Question> Questions { get; set; } = new();
        public ICommand AddFavoriteCommand => new Command(async () => await AddFavorite());

        public MainPageViewModel(QuestionService questionService)
        {
            this.questionService = questionService;
            OnLoad();
        }

        [ObservableProperty]
        private bool isRefreshing;

        private async Task AddFavorite()
        {
            await Shell.Current.DisplayAlert("Loaded", "Added success", "close");
        }

        private async void OnLoad()
        {
            await LoadQuestions();
            Title = "Study 100 Questions";
        }

        [RelayCommand]
        private async Task GoToQuestionDetail(Question question)
        {
            if (question == null)
                return;
            await Shell.Current.GoToAsync("QuestionDetailPage", true, new Dictionary<string, object> {
                { "Question", question }
            });
        }

        [RelayCommand]
        public async Task LoadQuestions()
        {
            try
            {
                IsBusy = true;
                var questions = await questionService.GetQuestions();

                if (Questions.Any())
                    Questions.Clear();

                foreach (var question in questions)
                {
                    Questions.Add(question);
                }
                
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", "Loaded with error", "close");
                throw;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}