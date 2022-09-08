using CivicMobile.ViewModels;

namespace CivicMobile.Views;

public partial class PracticePage : ContentPage
{
	public PracticePage(PracticePageViewModel practicePageViewModel)
	{
		InitializeComponent();
		BindingContext = practicePageViewModel;
	}
}