using CivicMobile.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using Xamarin.Essentials;
using System.Linq;
using ITextToSpeech = CivicMobile.Interfaces.ITextToSpeech;

namespace CivicMobile.Services
{
    public class TextToSpeechService : ITextToSpeech
    {
        public async Task Speak(string text, string language, CancellationTokenSource cancellationTokenSource=default)
        {
            if (cancellationTokenSource.IsCancellationRequested)
                return;
            IEnumerable<Locale> locales = (await TextToSpeech.GetLocalesAsync()).ToList();
            var speakLanguage = locales.FirstOrDefault(item => item.Country.Contains(language));
            await TextToSpeech.SpeakAsync(text, new SpeechOptions {Locale= speakLanguage },cancellationTokenSource.Token);

        }
    }
}
