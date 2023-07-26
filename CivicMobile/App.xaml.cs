namespace CivicMobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

#if WINDOWS
        // todo currently Splashscreen does not work on window,
        // create a story to implement window splashscreen
        var width = 600;
        var height = 800;

        window.Width = width;
        window.Height = height;
#endif

        return window;
    }
}