using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Scenario/Room", order = 1)]
public class Room : ScriptableObject
{
    [TextArea]
    public string text;
    public Exit[] exits;
}