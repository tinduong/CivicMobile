using CommunityToolkit.Mvvm.ComponentModel;

namespace CivicMobile.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<string> _availableLanguages;

        private string _selectedlanguage;

        public string SelectedLanguage
        {
            get => _selectedlanguage;
            set
            {
                var previous = _selectedlanguage;
                Preferences.Set("previousLanguage", previous);
                _selectedlanguage = value;
                Preferences.Set("selectionLanguage", value);
                OnPropertyChanged();
            }
        }

        public SettingsPageViewModel()
        {
            _availableLanguages = new() { "Vietnamese", "English" };
            SelectedLanguage = Preferences.Get("selectionLanguage", "English");
        }
    }
}