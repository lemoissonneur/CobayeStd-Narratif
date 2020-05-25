using UnityEngine;

[CreateAssetMenu(fileName = "Go", menuName = "Scenario/InputAction/Go")]
public class Go : InputAction
{
    public override void RespondToInput(string[] separatedInputWords)
    {
        //GameController.Instance.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
