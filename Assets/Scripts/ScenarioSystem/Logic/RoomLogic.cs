using System;
using System.Collections.Generic;

public class RoomLogic
{
    public Action<string> OnRoomChangedSuccessful;
    public Action<string> OnRoomChangedFailed;

    private readonly IRoomInterface roomInterface;
    private readonly RoomSimulation roomSimulation;
    private readonly Room startingRoom;

    public RoomLogic(IRoomInterface roomInterface, RoomSimulation roomSimulation)
    {
        this.roomInterface = roomInterface;
        this.roomSimulation = roomSimulation;

        startingRoom = roomInterface.CurrentRoom;
    }   

    public void AttemptToChangeRoom(string directionNoun)
    {
        if(roomSimulation.IsAnAvailableExit(directionNoun))
        {
            roomInterface.CurrentRoom = roomSimulation.UpdateRoom(directionNoun);
            OnRoomChangedSuccessful(directionNoun);
        }
        else
        {            
            OnRoomChangedFailed(directionNoun);
        }
    }

    public void Reset()
    {
        roomInterface.CurrentRoom = startingRoom;
        roomSimulation.ResetRoom(startingRoom);
    }

    public string RoomDescription()
    {
        return roomInterface.CurrentRoom.description;
    }    
}