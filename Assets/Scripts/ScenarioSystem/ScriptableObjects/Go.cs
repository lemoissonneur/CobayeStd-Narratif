using UnityEngine;

[CreateAssetMenu(fileName = "Go", menuName = "Scenario/InputAction/Go", order = 1000)]
public class Go : InputAction
{
    public override void RespondToInput(string[] separatedInputWords)
    {
        //RoomLogic.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
