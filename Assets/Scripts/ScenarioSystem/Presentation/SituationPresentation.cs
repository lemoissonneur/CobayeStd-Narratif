using System.Collections.Generic;
using UnityEngine.UI;

public class SituationPresentation
{
    public delegate void DisplayDelegate(bool visible);

    private DisplayDelegate _display;
    private readonly SituationLogic _situationLogic;
    private readonly Text _situationText;
    
    private static List<string> _actionLog = new List<string>();

    public SituationPresentation(DisplayDelegate display, SituationLogic situationLogic, Text situationText)
    {
        this._display = display;
        this._situationLogic = situationLogic;
        this._situationText = situationText;
    }

    private void LogStringWithReturn(string stringToAdd)
    {
        _actionLog.Add(stringToAdd + "\n");
    }

    private void UpdateDisplay()
    {
        string logAsText = string.Join("\n", _actionLog.ToArray());
        _situationText.text = logAsText;
    }    

    public void LogSituationText()
    {
        LogStringWithReturn(_situationLogic.GetCurrentSituation().Description + "\n");
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