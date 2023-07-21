using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}