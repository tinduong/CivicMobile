using Acr.UserDialogs;

using CivicMobile.Interfaces;

namespace CivicMobile.Services
{
    public class UserDialogService : IUserDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            //return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
            return Task.CompletedTask;
        }

        public void ShowNotification(string message)
        {
            //UserDialogs.Instance.Alert(message);
        }
    }
}
