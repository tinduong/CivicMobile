using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class QuestionDetailPage : ContentPage
{
    public QuestionDetailPage(QuestionDetailPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}