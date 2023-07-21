using CommunityToolkit.Mvvm.ComponentModel;

namespace CivicMobile.ViewModels;

public partial class WelcomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _examDate;

    [ObservableProperty]
    private List<string> _availableStates;

    private string _selectedState;

    public string SelectedState
    {
        get => _selectedState;
        set
        {
            var previous = _selectedState;
            //Preferences.Set("previousLanguage", previous);
            _selectedState = value;
            //Preferences.Set("selectionLanguage", value);
            OnPropertyChanged();
        }
    }

    public WelcomePageViewModel()
    {
        // get the exam date from the user preferences
        _availableStates = new()
        {
            "Alabama"
,"Alaska"
,"Arizona"
,"Arkansas"
,"California"
,"Colorado"
,"Connecticut"
,"Delaware"
,"Florida"
,"Georgia"
,"Hawaii"
,"Idaho"
,"Illinois"
,"Indiana"
,"Iowa"
,"Kansas"
,"Kentucky"
,"Louisiana"
,"Maine"
,"Maryland"
,"Massachusetts"
,"Michigan"
,"Minnesota"
,"Mississippi"
,"Missouri"
,"Montana"
,"Nebraska"
,"Nevada"
,"New Hampshire"
,"New Jersey"
,"New Mexico"
,"New York"
,"North Carolina"
,"North Dakota"
,"Ohio"
,"Oklahoma"
,"Oregon"
,"Pennsylvania"
,"Rhode Island"
,"South Caroli"
,"South Dakota"
,"Tennessee"
,"Texas"
,"Utah"
,"Vermont"
,"Virginia"
,"Washington"
,"West Virgini"
,"Wisconsin"
,"Wyoming"
        };
    }
}