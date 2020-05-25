using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Singleton form
    private static Game _instance;
    public static Game Instance { get { return _instance; } }

    [Header("Miscallenous")]
    public GameObject Room;
    public InputAction[] InputActions;

    private GameLogic gameLogic;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        RoomObject roomObject = Room.GetComponent<RoomObject>();
        if(!roomObject)
        {
            throw new UnityException("No display text defined. Please add a reference to a display text game object in Game.");
        }

        gameLogic = new GameLogic((routine) => StartCoroutine(routine), roomObject.RoomLogic, roomObject.RoomPresentation);
    }    
}
