using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivicMobile.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string ReadOutLoudSettings
        {
            get => AppSettings.GetValueOrDefault(nameof(ReadOutLoudSettings), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(ReadOutLoudSettings), value);
        }
    }
}
