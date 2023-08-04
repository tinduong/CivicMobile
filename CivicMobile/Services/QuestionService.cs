﻿using CivicMobile.Database;
using CivicMobile.Helpers;
using CivicMobile.Models;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CivicMobile.Services;

public class QuestionService
{
    private HttpClient _httpClient;
    private CivicDbContext _dbContext;

    public QuestionService(CivicDbContext dbContext)
    {
        _httpClient = new HttpClient();
        _dbContext = dbContext;
    }

    public async Task<List<Question>> GetQuestions()
    {
        var selectedLanguage = Preferences.Get(AppConstant.Settings_SelectedLanguage, Languages.ENG);
        var result = new List<Question>();
        string url;
        switch (selectedLanguage)
        {
            case Languages.ENG:
                url = $"{AppConstant.BaseResourceUrl}{AppConstant.File_Questions_ENG}";
                break;

            case Languages.VN:
                url = $"{AppConstant.BaseResourceUrl}{AppConstant.File_Questions_VN}";
                break;

            default:
                url = $"{AppConstant.BaseResourceUrl}{AppConstant.File_Questions_ENG}";
                break;
        }

        try
        {
            var response = await _httpClient.GetAsync(url);
            result = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<Question>>()
                                              : await TryGetQuestionsFromInternalAssest(AppConstant.File_Questions_ENG);
        }
        catch (Exception e)
        {
            await ShowOfflineMessage();
            result = await TryGetQuestionsFromInternalAssest(AppConstant.File_Questions_ENG);
        }

        return result;
    }

    private async Task<List<Question>> TryGetQuestionsFromInternalAssest(string fileName)
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using var reader = new StreamReader(stream);
            var result = JsonConvert.DeserializeObject<List<Question>>(await reader.ReadToEndAsync());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ShowOfflineMessage()
    {
        CancellationTokenSource cts = new();
        var toast = Toast.Make(AppConstant.OfflineMode, CommunityToolkit.Maui.Core.ToastDuration.Long);
        await toast.Show(cts.Token);
    }

    public async Task<List<Question>> GetExamQuestions()
    {
        var url = "https://raw.githubusercontent.com/tinduong/citizenship/master/PracticeTest_ENG.json";
        var result = new List<Question>();
        try
        {
            var response = await _httpClient.GetAsync(url);
            result = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<Question>>()
                                              : await TryGetQuestionsFromInternalAssest(AppConstant.File_Practice_ENG);
        }
        catch (Exception e)
        {
            await ShowOfflineMessage();
            result = await TryGetQuestionsFromInternalAssest(AppConstant.File_Practice_ENG);
        }
        return result;
    }
}