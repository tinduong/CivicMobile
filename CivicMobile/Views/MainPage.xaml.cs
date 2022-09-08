using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
    }
}