namespace CivicMobile.Interfaces;

public interface ITextToSpeech
{
    Task Speak(string text, string language, CancellationTokenSource cancellationTokenSource = default);
}