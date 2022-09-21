using CivicMobile.Models;
using CivicMobile.Views.Template;

namespace CivicMobile.Helpers;

public class PracticeTestDataTemplateSelector : DataTemplateSelector
{
    private readonly DataTemplate normalTestTemplate, correctAnswerTempate, wrongAnswerTempate;

    public PracticeTestDataTemplateSelector()
    {
        normalTestTemplate = new DataTemplate(typeof(NormalTestTemplate));
        correctAnswerTempate = new DataTemplate(typeof(CorrectAnswerTemplate));
        wrongAnswerTempate = new DataTemplate(typeof(WrongAnswerTemplate));
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var answer = (Answer)item;
        if (!answer.IsQuestionAnswered)
        {
            // use normal template
            return normalTestTemplate;
        }
        else if (answer.IsQuestionAnswered && answer.IsCorrect)
        {
            return correctAnswerTempate;
        }
        else if (answer.IsQuestionAnswered && !answer.IsCorrect && answer.IsSelectedAnswer)
        {
            return wrongAnswerTempate;
        }
        else
        {
            return normalTestTemplate;
        }
    }
}