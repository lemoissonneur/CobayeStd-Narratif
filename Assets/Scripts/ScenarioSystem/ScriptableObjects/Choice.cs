using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Choice", menuName = "Scenario/Choice", order = 1)]
public class Choice : ScriptableObject
{
    public string KeyString;
    [TextArea]
    public string ChoiceDescription;
    public Situation NextSituation;
}
