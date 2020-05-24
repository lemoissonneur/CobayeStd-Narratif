using UnityEngine;

[RequireComponent(typeof(GameController))]
public class ScenarioNodeNavigation : MonoBehaviour
{
    public ScenarioNode currentNode;

    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackPlayerChoicesInScenarioNode()
    {
        for (int i = 0; i < currentNode.playerChoices.Length; i++)
        {
            controller.playerChoicesInScenarioNode.Add(currentNode.playerChoices[i].text);
        }
    }
}