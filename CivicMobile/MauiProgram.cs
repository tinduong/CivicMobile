using CivicMobile.Interfaces;
using CivicMobile.Services;
using CivicMobile.ViewModels;
using CivicMobile.Views;
using CommunityToolkit.Maui;

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

        builder.Services.AddTransient<QuestionService>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<PracticePage>();
        builder.Services.AddTransient<PracticePageViewModel>();

        builder.Services.AddTransient<QuestionDetailPage>();
        builder.Services.AddTransient<QuestionDetailPageViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();

        builder.Services.AddTransient<IAudioPlayer, AudioPlayerService>();

        return builder.Build();
    }
}