using System;
using System.Collections.Generic;

public class RoomSimulation
{
    private Room room;

    private readonly List<string> interactionDescriptionsInRoom = new List<string>();
    private readonly Dictionary<string, Room> exitDictionnary = new Dictionary<string, Room>();

    public RoomSimulation(Room startingRoom)
    {
        this.room = startingRoom;
    }

    public Room UpdateRoom(string directionNoun)
    {
        room = exitDictionnary[directionNoun];

        // Clear collections for new room
        interactionDescriptionsInRoom.Clear();
        exitDictionnary.Clear();

        // Unpack exits in current room
        for (int i = 0; i < room.exits.Length; i++)
        {
            exitDictionnary.Add(room.exits[i].keyString, room.exits[i].valueRoom);
            interactionDescriptionsInRoom.Add(room.exits[i].exitDescription);
        }

        return room;
    }

    public void ResetRoom(Room room)
    {

    }

    public bool IsAnAvailableExit(string directionNoun)
    {
        return exitDictionnary.ContainsKey(directionNoun);
    }
}