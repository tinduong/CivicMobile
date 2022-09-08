using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CivicMobile.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;
       
       

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public string GetItem(string key)
        {
            try
            {
                var item = _settings.GetValueOrDefault(key, string.Empty);
                return item;
            }
            catch (System.Exception e)
            {

                throw;
            }
           
        }

    }

}
