using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Situation))]
public class SituationEditor : Editor
{
    private Situation situation;

    private SerializedProperty situationTypeProperty;
    private SerializedProperty descriptionProperty;
    private SerializedProperty nextSituationProperty;
    private SerializedProperty choicesProperty;

    private const string situationPropSituationType = "situationType";
    private const string situationPropDescription = "description";
    private const string situationPropNextSituation = "nextSituation";
    private const string situationPropChoices = "choices";

    private void OnEnable()
    {
        situation = (Situation)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        descriptionProperty = serializedObject.FindProperty(situationPropDescription);
        situationTypeProperty = serializedObject.FindProperty(situationPropSituationType);
        nextSituationProperty = serializedObject.FindProperty(situationPropNextSituation);
        choicesProperty = serializedObject.FindProperty(situationPropChoices);
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        /*DrawPropertiesExcluding(serializedObject, new string[]{ situationPropNextSituation, situationPropChoices });
        
        switch(situation.type)
        {
            case Situation.SituationType.TextInteractive:
                
                break;

            case Situation.SituationType.TextOnly:
                //nextSituationProperty.
                break;

            default:
                throw new UnityException(situation.type + " not handled by SituationEditor script.");
        }*/
    }
}
