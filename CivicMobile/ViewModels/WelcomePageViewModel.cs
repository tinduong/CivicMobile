using CommunityToolkit.Mvvm.ComponentModel;

namespace CivicMobile.ViewModels;

public partial class WelcomePageViewModel : BaseViewModel
{
    public WelcomePageViewModel(SettingsPageViewModel settings)
    {
        settings.DarkMode = Preferences.Get("darkMode", false);
        settings.AgeMode = Preferences.Get("ageMode", false);
    }
}