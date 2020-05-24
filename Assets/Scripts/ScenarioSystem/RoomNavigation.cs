using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    private GameController controller;
    private Dictionary<string, Room> exitDictionnary = new Dictionary<string, Room>();

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionnary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }
}