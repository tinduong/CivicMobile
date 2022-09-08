using CommunityToolkit.Mvvm.ComponentModel;

namespace CivicMobile.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<string> _availableLanguages;

        [ObservableProperty]
        private string _selectedLanguage;

        public SettingsPageViewModel()
        {
            _availableLanguages = new() { "Vietnamese", "English" };
        }
    }
}