using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameController))]
public class TextInput : MonoBehaviour
{
    public InputField inputField;

    private void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    
    private void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        GameController.Instance.LogStringWithReturn(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < GameController.Instance.inputActions.Length;i++)
        {
            InputAction inputAction = GameController.Instance.inputActions[i];
            if(inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(separatedInputWords);
            }

        }


        InputComplete();
    }

    private void InputComplete()
    {
        GameController.Instance.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
