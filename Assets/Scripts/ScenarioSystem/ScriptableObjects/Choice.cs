﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Choice", menuName = "Scenario/Choice", order = 1)]
public class Choice : ScriptableObject
{
    public string keyString;
    [TextArea]
    public string choiceDescription;
    public Situation nextSituation;
}
