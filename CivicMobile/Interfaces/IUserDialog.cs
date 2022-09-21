using CivicMobile.Models;

namespace CivicMobile.Interfaces;

public interface IDataService
{
    Task<IEnumerable<Question>> LoadQuestions();
}

public interface IUserDialogService
{
    Task ShowDialog(string message, string title, string buttonLabel);

    void ShowNotification(string message);
}