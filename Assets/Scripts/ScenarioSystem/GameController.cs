using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RoomNavigation))]
public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public Text displayText;
    public InputAction[] inputActions;        

    [HideInInspector]
    public RoomNavigation roomNavigation;
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    private List<string> actionLog = new List<string>();

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayRoomText()
    {
        // Clear collections for new room
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();

        // Display room text
        roomNavigation.UnpackExitsInRoom();
        string combinedText = roomNavigation.currentRoom.description + "\n";
        LogStringWithReturn(combinedText);
    }

    public void DisplayLoggedText()
    {
        // Display logged text
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
}
