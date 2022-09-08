using CivicMobile.Models;
using System.Net.Http.Json;

namespace CivicMobile.Services
{
    public class QuestionService
    {
        private HttpClient _httpClient;

        public QuestionService()
        {
            _httpClient = new HttpClient();
        }

        private List<Question> questionList = new();
        private List<Question> examList = new();

        public async Task<List<Question>> GetQuestions()
        {
            if (questionList.Count > 0)
                return questionList;

            // call api
            var url = "https://raw.githubusercontent.com/tinduong/citizenship/master/Questions_ENG.json";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                questionList = await response.Content.ReadFromJsonAsync<List<Question>>();
            }

            return questionList;
        }

        public async Task<List<Question>> GetExamQuestions()
        {
            if (examList.Count > 0)
                return examList;

            // call api
            var url = "https://raw.githubusercontent.com/tinduong/citizenship/master/PracticeTest_ENG.json";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                examList = await response.Content.ReadFromJsonAsync<List<Question>>();
            }

            return examList;
        }
    }
}