using System.Collections.Generic;
using UnityEngine.UI;

public class SituationPresentation
{
    public delegate void DisplayDelegate(bool visible);

    private DisplayDelegate display;
    private readonly SituationLogic situationLogic;
    private readonly Text situationText;
    
    private static List<string> actionLog = new List<string>();

    public SituationPresentation(DisplayDelegate display, SituationLogic situationLogic, Text situationText)
    {
        this.display = display;
        this.situationLogic = situationLogic;
        this.situationText = situationText;
    }

    private void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    private void UpdateDisplay()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        situationText.text = logAsText;
    }    

    public void LogSituationText()
    {
        LogStringWithReturn(situationLogic.GetCurrentSituation().description + "\n");
        UpdateDisplay();
    }

    public void LogSituationChangedSuccess(string directionNoun)
    {
        LogStringWithReturn("You head off to the " + directionNoun);
        LogSituationText();
        UpdateDisplay();
    }

    public void LogSituationChangedFailure(string directionNoun)
    {
        LogStringWithReturn("There is no path to the " + directionNoun);
        UpdateDisplay();
    }

    public void LogUserInput(string userInput)
    {
        LogStringWithReturn(userInput);
        UpdateDisplay();
    }
}