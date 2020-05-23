using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "", order = 1)]
[System.Serializable]
public class Scenario : ScriptableObject
{
    [SerializeField]
    public LinkedList<TextNode> textNodes;
}
