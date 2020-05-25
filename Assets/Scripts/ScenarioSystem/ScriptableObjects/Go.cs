using UnityEngine;

[CreateAssetMenu(fileName = "Go", menuName = "Scenario/InputAction/Go")]
public class Go : InputAction
{
    public override void RespondToInput(string[] separatedInputWords)
    {
        //RoomLogic.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
