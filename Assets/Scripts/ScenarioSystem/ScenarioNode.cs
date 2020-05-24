using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioNode", menuName = "Scenario/ScenarioNode", order = 1)]
public class ScenarioNode : ScriptableObject
{
    [TextArea]
    public string text;
    public PlayerChoice[] playerChoices;
}