using CivicMobile.Services;
using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class WelcomePage : ContentPage
{
    private WelcomePageViewModel _viewModel;
    private ISettingService _settingService;

    public WelcomePage(WelcomePageViewModel viewModel, ISettingService settingService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _settingService = settingService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}