using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice : ScriptableObject
{
    public string KeyString;
    [TextArea]
    public string Description;
    public TextFormatSettings Format;
    public Situation NextSituation;
}
