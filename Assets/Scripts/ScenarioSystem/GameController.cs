using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RoomNavigation))]
public class GameController : MonoBehaviour
{
    public Text displayText;
    public InputAction[] inputActions;        

    [HideInInspector]
    public RoomNavigation roomNavigation;
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    private List<string> actionLog = new List<string>();

    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        roomNavigation.UnpackExitsInRoom();

        string combinedText = roomNavigation.currentRoom.description + "\n";

        LogStringWithReturn(combinedText);
    }

    public void DisplayPlayerChoices()
    {           
        string joinedPlayerChoices = string.Join("\n", interactionDescriptionsInRoom.ToArray());
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    private void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }
}
