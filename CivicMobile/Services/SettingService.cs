namespace CivicMobile.Services
{
    public interface ISettingService
    {
        void AddItem(string key, string value);

        string GetItem(string key, string defaultValue);

        void GetAll();
    }

    public class SettingService : ISettingService
    {
        private readonly IPreferences _setting;

        public SettingService()
        {
            _setting = Preferences.Default;
        }

        public void AddItem(string key, string value)
        {
            _setting.Set(key, value);
        }

        public void GetAll()
        {
            var value = _setting.Get("ageMode", false);
            var value2 = _setting.Get("darkMode", false);
            var value3 = _setting.Get("selectionLanguage", "English");
        }

        public string GetItem(string key, string defaultValue)
        {
            try
            {
                var item = _setting.Get(key, defaultValue);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}