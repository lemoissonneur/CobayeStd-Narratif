using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scenario/Scenario", order = 1)]
[System.Serializable]
public class Scenario : ScriptableObject
{
    public Situation[] situations;
}
