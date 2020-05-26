using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Choice))]
public class ChoiceEditor : Editor
{
    private Choice choice;

    private void OnEnable()
    {
        choice = (Choice)target;
        
        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }

        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
