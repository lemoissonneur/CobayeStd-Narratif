using UnityEngine;

[CreateAssetMenu(fileName = "Go", menuName = "Scenario/InputAction/Go")]
public class Go : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
