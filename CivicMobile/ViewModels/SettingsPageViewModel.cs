using CivicMobile.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

//using CommunityToolkit.Mvvm.Input;

namespace CivicMobile.ViewModels;

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

    private bool _ageMode;

    public bool AgeMode
    {
        get => _ageMode;
        set
        {
            _ageMode = value;
            Preferences.Set("ageMode", value);
            OnPropertyChanged();
        }
    }

    private bool _darkMode;

    public bool DarkMode
    {
        get => _darkMode;
        set
        {
            _darkMode = value;
            Preferences.Set("darkMode", value);
            App.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
            OnPropertyChanged();
        }
    }

    // ResetPreferenceCommand
    [RelayCommand]
    private void ResetPreference()
    {
        DarkMode = false;
        AgeMode = false;
        SelectedLanguage = "English";
        Preferences.Clear();
    }

    public SettingsPageViewModel()
    {
        _availableLanguages = new() { "Vietnamese", "English" };
        SelectedLanguage = Preferences.Get(AppConstant.Settings_SelectedLanguage, "English");
        DarkMode = Preferences.Get(AppConstant.Settings_DarkMode, false);
        AgeMode = Preferences.Get(AppConstant.Settings_AgeMode, false);
    }
}