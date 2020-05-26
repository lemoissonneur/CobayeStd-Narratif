using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Miscallenous")]
    public GameObject SituationObject;

    private GameLogic _gameLogic;
    private SituationHandler situationHandler;

    private void Awake()
    {
        situationHandler = SituationObject.GetComponent<SituationHandler>();
        if(!situationHandler)
        {
            throw new UnityException("No display text defined. Please add a reference to a display text game object in Game.");
        }        
    }

    private void Start()
    {
        _gameLogic = new GameLogic((routine) => StartCoroutine(routine), situationHandler.SituationLogic, situationHandler.SituationPresentation);
    }
}
