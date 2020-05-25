using UnityEngine;
using UnityEngine.UI;

public class RoomObject : MonoBehaviour, IRoomInterface
{
    public Room StartingRoom;

    [HideInInspector]
    public RoomLogic RoomLogic { get; private set; }

    [HideInInspector]
    public RoomPresentation RoomPresentation { get; private set; }

    public Room CurrentRoom
    {
        get
        {
            return currentRoom;
        }
        set
        {
            currentRoom = value;
        }
    }
    private Room currentRoom;
    private RoomSimulation roomSimulation;

    private void Awake()
    {
        Text roomText = GetComponentsInChildren<Text>()[0];

        currentRoom = StartingRoom;
        roomSimulation = new RoomSimulation(currentRoom);
        RoomLogic = new RoomLogic(this, roomSimulation);
        RoomPresentation = new RoomPresentation((visible) => gameObject.SetActive(visible), RoomLogic, roomText);

        RoomLogic.OnRoomChangedSuccessful += (directionNoun) => roomSimulation.UpdateRoom(directionNoun);
        RoomLogic.OnRoomChangedSuccessful += (directionNoun) => RoomPresentation.LogStringWithReturn("You head off to the " + directionNoun);
        RoomLogic.OnRoomChangedSuccessful += (directionNoun) => RoomPresentation.UpdateText();

        RoomLogic.OnRoomChangedFailed += (directionNoun) => RoomPresentation.LogStringWithReturn("There is no path to the " + directionNoun);
        RoomLogic.OnRoomChangedSuccessful += (directionNoun) => RoomPresentation.UpdateText();
    }

    private void OnRoomChangeAttempted(string directionNoun)
    {
        RoomLogic.AttemptToChangeRoom(directionNoun);           
    }
}
