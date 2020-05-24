using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text displayText;

    [HideInInspector]
    public ScenarioNodeNavigation scenarioNavigation;

    private List<string> actionLog = new List<string>();

    private void Awake()
    {
        scenarioNavigation = GetComponent<ScenarioNodeNavigation>();
    }

    private void Start()
    {
        DisplayScenarioNodeText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void DisplayScenarioNodeText()
    {
        string combinedText = scenarioNavigation.currentNode.text + "\n";

        LogStringWithReturn(combinedText);
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
}
