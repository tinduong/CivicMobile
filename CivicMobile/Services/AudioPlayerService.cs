using CivicMobile.Interfaces;
using Plugin.SimpleAudioPlayer;
using System.Reflection;

namespace CivicMobile.Services
{
    public class AudioPlayerService : IAudioPlayer
    {
        ISimpleAudioPlayer _player =  Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
        public AudioPlayerService()
        {
           
        }
        public void Play(string fileName)
        {
            _player.Load(GetStreamFromFile(fileName));
            _player.Play();
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("CivicMobile.Audio." + filename);
            return stream;
        }
    }

    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);
    }

}
