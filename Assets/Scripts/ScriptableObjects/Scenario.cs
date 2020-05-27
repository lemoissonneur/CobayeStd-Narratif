using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scenario", order = 1)]
[Serializable]
public class Scenario : ScriptableObject
{
    public Situation[] Situations;
}
