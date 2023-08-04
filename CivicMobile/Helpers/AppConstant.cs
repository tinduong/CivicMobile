using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivicMobile.Helpers
{
    public static class AppConstant
    {
        public static string Settings_SelectedLanguage = "selectionLanguage";
        public static string Settings_DarkMode = "darkMode";
        public static string Settings_AgeMode = "ageMode";
        public static string File_Questions_ENG = "Questions_ENG.json";
        public static string File_Questions_VN = "Questions_VN.json";
        public static string File_Practice_ENG = "PracticeTest_ENG.json";
        public static string BaseResourceUrl = "https://raw.githubusercontent.com/tinduong/citizenship/master/";

        // make your are offline
        public static string OfflineMode = "You are offline, Offline mode only supports English";
    }

    public static class Languages
    {
        public const string ENG = "English";
        public const string VN = "Vietnamese";
    }
}