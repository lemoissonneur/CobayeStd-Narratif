using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameController))]
public class PlayerInput : MonoBehaviour
{
    public GameObject choicesArea;

    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();         
    }

    private void Start()
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();

        for (int i = 0; i < controller.playerChoicesInScenarioNode.Count; i++)
        {
            GameObject uiBtton = DefaultControls.CreateButton(uiResources);
            /*Button button = uiBtton.AddComponent<Button>();
            button.name = controller.playerChoicesInScenarioNode[i];*/
            uiBtton.transform.SetParent(choicesArea.transform);
            Debug.Log(controller.playerChoicesInScenarioNode[i]);
        }
    }
}
