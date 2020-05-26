using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Situation))]
public class SituationEditor : Editor
{
    private Situation _situation;

    private SerializedProperty _situationTypeProperty;
    private SerializedProperty _descriptionProperty;
    private SerializedProperty _nextSituationProperty;
    private SerializedProperty _choicesProperty;

    private const string situationPropSituationType = "situationType";
    private const string situationPropDescription = "description";
    private const string situationPropNextSituation = "nextSituation";
    private const string situationPropChoices = "choices";

    private void OnEnable()
    {
        _situation = (Situation)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        _descriptionProperty = serializedObject.FindProperty(situationPropDescription);
        _situationTypeProperty = serializedObject.FindProperty(situationPropSituationType);
        _nextSituationProperty = serializedObject.FindProperty(situationPropNextSituation);
        _choicesProperty = serializedObject.FindProperty(situationPropChoices);
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
