using Nancy.TinyIoc;

namespace CivicMobile
{
    public static class RegistrationService
    {
        private static TinyIoCContainer container = new TinyIoCContainer();
        public static void RegisterDependencies()
        {
            if (container == null)
                container = new TinyIoCContainer();

            // Register all services
            //container.Register<IUserDialogService, UserDialogService>();
            //container.Register<IAudioPlayer, AudioPlayerService>();
            //container.Register<IDataStore<Question>, MockDataStore>();
            //container.Register<ITextToSpeech, TextToSpeechService>();
            //container.Register<ISettingsService, SettingsService>();
            //container.Register<IMapService, MapService>();

            // Register ViewModel
            //container.Register<SettingViewModel>();
            //container.Register<TestCenterViewModel>();
            //container.Register<PracticeTestViewModel>();

        }

        public static object Resolve(Type typeName)
        {
            return container.Resolve(typeName);
        }
    }
}
