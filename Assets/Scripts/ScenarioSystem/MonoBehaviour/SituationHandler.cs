using UnityEngine;
using UnityEngine.UI;

public class SituationHandler : MonoBehaviour, ISituationInterface
{
    public Situation StartingSituation;

    [Header("Situation references")]
    [Tooltip("Reference to the Text GameObject Component which will handle the situation text")]
    public Text SituationText;
    [Tooltip("Reference to the GameObject which will handle the player choices display")]
    public InputField PlayerChoice;

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
            return _currentSituation;
        }
        set
        {
            _currentSituation = value;
        }
    }
    private Situation _currentSituation;
    private SituationSimulation _situationSimulation;
    private IPlayerInput _playerInput;

    private readonly static char[] _delimiterCharacters = { ' ' };

    private void Awake()
    {
        _playerInput = new PlayerInput(PlayerChoice);
        _playerInput.OnAcceptedStringInput += (userInput) => SituationPresentation.LogUserInput(userInput);
        _playerInput.OnAcceptedStringInput += (userInput) => OnSituationChangeAttempt(userInput);

        _currentSituation = StartingSituation;
        _situationSimulation = new SituationSimulation(_currentSituation);
        SituationLogic = new SituationLogic(this, _situationSimulation);
        SituationPresentation = new SituationPresentation((visible) => gameObject.SetActive(visible), SituationLogic, SituationText);

        SituationLogic.OnSituationChangedSuccess += (directionNoun) => SituationPresentation.LogSituationChangedSuccess(directionNoun);

        SituationLogic.OnSituationChangedFailed += (directionNoun) => SituationPresentation.LogSituationChangedFailure(directionNoun);
    }

    private void OnSituationChangeAttempt(string userInput)
    {
        string[] separatedInputWords = userInput.Split(_delimiterCharacters);
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
