using CivicMobile.Views;
using System.Windows.Input;

namespace CivicMobile;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
        BindingContext = this;
    }

    private void RegisterRoutes()
    {
        Routes.Add("PracticePage", typeof(PracticePage));
        Routes.Add("MainPage", typeof(MainPage));
        Routes.Add(nameof(QuestionDetailPage), typeof(QuestionDetailPage));
        Routes.Add(nameof(WelcomePage), typeof(WelcomePage));
        Routes.Add(nameof(SettingsPage), typeof(SettingsPage));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}