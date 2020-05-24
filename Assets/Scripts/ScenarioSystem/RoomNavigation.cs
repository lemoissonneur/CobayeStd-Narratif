using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;
    private Dictionary<string, Room> exitDictionnary = new Dictionary<string, Room>();

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionnary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            GameController.Instance.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void AttemptToChangeRooms(string directionNoun) 
    {
        if(exitDictionnary.ContainsKey(directionNoun))
        {
            currentRoom = exitDictionnary[directionNoun];
            GameController.Instance.LogStringWithReturn("You head off to the " + directionNoun);
            GameController.Instance.DisplayRoomText();
        }
        else
        {
            GameController.Instance.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        exitDictionnary.Clear();
    }
}