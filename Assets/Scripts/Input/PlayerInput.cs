using System;
using UnityEngine.UI;

public class PlayerInput : IPlayerInput
{
    public Action<string> OnAcceptedStringInput
    {
        get
        {
            return onAcceptedStringInput;
        }
        set
        {
            onAcceptedStringInput = value;
        }
    }
        
    public string TextInput => inputField.text;

    private Action<string> onAcceptedStringInput;
    private readonly InputField inputField;
    
    public PlayerInput(InputField inputField)
    {
        this.inputField = inputField;
        this.inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    public void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        
        OnAcceptedStringInput(userInput);        

        inputField.ActivateInputField();
        inputField.text = null;
    }
}
