using System.Reflection;
using CivicMobile.Models;
using Newtonsoft.Json;

namespace CivicMobile.Services
{
    public class MockDataStore : IDataStore<Question>
    {
        private ISettingsService _settingService;

        public MockDataStore(ISettingsService settingService)
        {
            _settingService = settingService;
        }

        public async Task<bool> AddItemAsync(Question item)
        {
          

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Question item)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Question> GetItemAsync(string id)
        {
            return await Task.FromResult(new Question());
        }

        public async Task<IEnumerable<Question>> GetItemsAsync(bool forceRefresh = false)
        {

            var language = _settingService.GetItem("SelectedLanguage");

            // try to read question from json files if the data in the database does not exists
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(RegistrationService)).Assembly;
            Stream stream = null;
            //var languageFile = $"CivicMobile.Questions_{language}.json";
            // prepare file for available language

            if (language == "VN")// todo remove hard code, use convenction or mapping
            {
                 stream = assembly.GetManifestResourceStream("CivicMobile.Questions_VN.json");
            }
            else if (language == "ES")
            {
                stream = assembly.GetManifestResourceStream("CivicMobile.Questions_ES.json");
            }
            else
            {
                 stream = assembly.GetManifestResourceStream("CivicMobile.Questions_ENG.json");
            }
            

            var questions = new List<Question>();
            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<Question>>(json);
            }
           
            return await Task.FromResult(questions);
        }

        public async Task<IEnumerable<Question>> GetPracticeTestItemsAsync(int testSize)
        {
            var language = _settingService.GetItem("SelectedLanguage");

            // try to read question from json files if the data in the database does not exists
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(RegistrationService)).Assembly;
            
            var stream = assembly.GetManifestResourceStream("CivicMobile.PracticeTest_ENG.json");


            var questions = new List<Question>();
            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<Question>>(json);
            }

            return await Task.FromResult(questions);
        }
    }
}