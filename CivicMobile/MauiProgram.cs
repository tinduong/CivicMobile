using CivicMobile.Database;
using CivicMobile.Services;
using CivicMobile.ViewModels;
using CivicMobile.Views;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;

namespace CivicMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiCommunityToolkit();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // register services
        builder.Services.AddTransient<QuestionService>();
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<CivicDbContext>();

        // register pages and page viewmodels
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<PracticePage>();
        builder.Services.AddTransient<PracticePageViewModel>();

        builder.Services.AddTransient<QuestionDetailPage>();
        builder.Services.AddTransient<QuestionDetailPageViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();

        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<WelcomePageViewModel>();

        return builder.Build();
    }
}