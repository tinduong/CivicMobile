using CivicMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CivicMobile.ViewModels
{
    [QueryProperty("Question", "Question")]
    public partial class QuestionDetailPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Question question;

        public QuestionDetailPageViewModel()
        {
            var test = Question;
            if(Question != null)
            {
                Title = $"Question Number {Question.QuestionNumber}";
            }
        }
    }
}