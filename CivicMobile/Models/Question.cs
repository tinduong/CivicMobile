using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CivicMobile.Models;

public class Answer : INotifyPropertyChanged
{
    private string _answerText;
    private bool _isCorrect;
    private bool _isSelectedAnswer;
    private bool _isQuestionAnswered;

    public string AnswerText
    {
        get { return _answerText; }
        set { _answerText = value; OnPropertyChanged("AnswerText"); }
    }

    public bool IsCorrect
    {
        get { return _isCorrect; }
        set { _isCorrect = value; OnPropertyChanged("IsCorrect"); }
    }

    public bool IsQuestionAnswered
    {
        get { return _isQuestionAnswered; }
        set { _isQuestionAnswered = value; OnPropertyChanged("IsQuestionAnswered"); }
    }

    public bool IsSelectedAnswer
    {
        get { return _isSelectedAnswer; }
        set { _isSelectedAnswer = value; OnPropertyChanged("IsSelectedAnswer"); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}

public class Question : INotifyPropertyChanged
{
    public Question()
    {
        Answers = new ObservableCollection<Answer>();
    }

    public int QuestionNumber { get; set; }
    public string QuestionDescription { get; set; }

    private ObservableCollection<Answer> _answers;

    public ObservableCollection<Answer> Answers
    {
        get { return _answers; }
        set { _answers = value; OnPropertyChanged("Answers"); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}