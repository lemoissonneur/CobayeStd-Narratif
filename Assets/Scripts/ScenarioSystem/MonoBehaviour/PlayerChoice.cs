using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerChoice : MonoBehaviour
{
    private InputField inputField;

    private readonly static char[] delimiterCharacters = { ' ' };

    private void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    public void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();

        // TODO : c'est trèèèèèèèèèèès moche => à améliorer
        //RoomPresentation.LogStringWithReturn(userInput);
        
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        // TODO : ca aussi d'ailleurs => à améliorer
        for (int i = 0; i < Game.Instance.InputActions.Length; i++)
        {
            InputAction inputAction = Game.Instance.InputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(separatedInputWords);
            }

        }

        inputField.ActivateInputField();
        inputField.text = null;
    }
}
