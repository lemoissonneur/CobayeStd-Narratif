using UnityEngine;
using UnityEngine.UI;

public class SituationHandler : MonoBehaviour, ISituationInterface
{
    public Situation StartingSituation;

    [Header("Situation references")]
    [Tooltip("Reference to the Text GameObject Component which will handle the situation text")]
    public Text situationText;
    [Tooltip("Reference to the GameObject which will handle the player choices display")]
    public InputField playerChoice;

    [Header("Actions available")]
    public InputAction[] InputActions;

    [HideInInspector]
    public SituationLogic SituationLogic { get; private set; }

    [HideInInspector]
    public SituationPresentation SituationPresentation { get; private set; }    

    public Situation CurrentSituation
    {
        get
        {
            return currentSituation;
        }
        set
        {
            currentSituation = value;
        }
    }
    private Situation currentSituation;
    private SituationSimulation situationSimulation;
    private IPlayerInput playerInput;

    private readonly static char[] delimiterCharacters = { ' ' };

    private void Awake()
    {
        playerInput = new PlayerInput(playerChoice);
        playerInput.OnAcceptedStringInput += (userInput) => SituationPresentation.LogUserInput(userInput);
        playerInput.OnAcceptedStringInput += (userInput) => OnSituationChangeAttempt(userInput);

        currentSituation = StartingSituation;
        situationSimulation = new SituationSimulation(currentSituation);
        SituationLogic = new SituationLogic(this, situationSimulation);
        SituationPresentation = new SituationPresentation((visible) => gameObject.SetActive(visible), SituationLogic, situationText);

        SituationLogic.OnSituationChangedSuccess += (directionNoun) => SituationPresentation.LogSituationChangedSuccess(directionNoun);

        SituationLogic.OnSituationChangedFailed += (directionNoun) => SituationPresentation.LogSituationChangedFailure(directionNoun);
    }

    private void OnSituationChangeAttempt(string userInput)
    {
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
        for (int i = 0; i < InputActions.Length; i++)
        {
            InputAction inputAction = InputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {
                SituationLogic.AttemptToChangeSituation(separatedInputWords[1]);
                return;
            }
        }                   
    }
}
