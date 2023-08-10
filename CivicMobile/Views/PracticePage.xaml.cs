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

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}