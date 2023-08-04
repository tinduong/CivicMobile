using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class PracticePage : ContentPage
{
    private PracticePageViewModel ViewModel;

    public PracticePage(PracticePageViewModel practicePageViewModel)
    {
        InitializeComponent();
        ViewModel = practicePageViewModel;
        BindingContext = practicePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await ViewModel.LoadExamQuestions();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // make sure you can really want to go else keep you here
    }
}