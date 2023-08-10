using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CivicMobile.Models
{
    public interface IContextModel
    {
    }

    //public class UserPreference
    //{
    //    public string Language { get; set; }
    //    public int Age { get; set; }
    //    public string State { get; set; }
    //    public DateTime? RegisteredExamDate { get; set; }
    //}

    public class UserRecord : IContextModel
    {
        [PrimaryKey]
        public int QuestionNumber { get; set; }

        public int PracticeCount { get; set; } = 0;
        public int CorrectCount { get; set; } = 0;
        public int WrongCount { get; set; } = 0;
        public bool isFavorite { get; set; } = false;
        public bool isMarkedForReview { get; set; } = false;
    }

    public class UserPreference
    {
        public bool AgeMode { get; set; }
        public bool DarkMode { get; set; }
        public string Language { get; set; }
    }
}