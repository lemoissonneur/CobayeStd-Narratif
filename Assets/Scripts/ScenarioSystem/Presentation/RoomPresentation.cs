using System.Collections.Generic;
using UnityEngine.UI;

public class RoomPresentation
{
    public delegate void DisplayDelegate(bool visible);

    private DisplayDelegate display;
    private readonly RoomLogic roomLogic;
    private readonly Text roomText;
    
    private static List<string> actionLog = new List<string>();

    public RoomPresentation(DisplayDelegate display, RoomLogic roomLogic, Text roomText)
    {
        this.display = display;
        this.roomLogic = roomLogic;
        this.roomText = roomText;
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public void UpdateText()
    {
        // Display room text
        string combinedText = roomLogic.RoomDescription() + "\n";
        LogStringWithReturn(combinedText);

        // Display logged text
        string logAsText = string.Join("\n", actionLog.ToArray());
        roomText.text = logAsText;
    }    
}