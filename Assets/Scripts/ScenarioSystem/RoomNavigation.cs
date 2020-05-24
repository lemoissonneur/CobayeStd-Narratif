using UnityEngine;

[RequireComponent(typeof(GameController))]
public class RoomNavigation : MonoBehaviour
{
    public Room currentNode;

    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentNode.exits.Length; i++)
        {
            controller.exitsInRoom.Add(currentNode.exits[i].text);
        }
    }
}