using ITextToSpeech = CivicMobile.Interfaces.ITextToSpeech;

namespace CivicMobile.Services;

public class TextToSpeechService : ITextToSpeech
{
    public TextToSpeechService()
    {
    }

    public async Task Speak(string text, string language, CancellationTokenSource? cancellationTokenSource)
    {
        if (cancellationTokenSource.IsCancellationRequested)
            return;
        IEnumerable<Locale> locales = (await TextToSpeech.GetLocalesAsync()).ToList();
        var speakLanguage = locales.FirstOrDefault(item => item.Country.Contains(language));
        await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions { Locale = speakLanguage });
    }
}