using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _ViewModel;

    public MainPage(MainPageViewModel homePageViewModel)
    {
        InitializeComponent();
        _ViewModel = homePageViewModel;
        BindingContext = homePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await _ViewModel.LoadQuestions();
    }
}