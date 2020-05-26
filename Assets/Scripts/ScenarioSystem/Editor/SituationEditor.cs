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
    private SerializedProperty _formatProperty;
    private SerializedProperty _nextSituationProperty;
    private SerializedProperty _choicesProperty;

    private const string situationPropSituationType = "Type";
    private const string situationPropDescription = "Description";
    private const string situationPropFormat = "Format";
    private const string situationPropNextSituation = "NextSituation";
    private const string situationPropChoices = "Choices";

    private void OnEnable()
    {
        _situation = (Situation)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        _situationTypeProperty = serializedObject.FindProperty(situationPropSituationType);
        _descriptionProperty = serializedObject.FindProperty(situationPropDescription);
        _formatProperty = serializedObject.FindProperty(situationPropFormat);
        _nextSituationProperty = serializedObject.FindProperty(situationPropNextSituation);
        _choicesProperty = serializedObject.FindProperty(situationPropChoices);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DisplaySituationDefinition();
        DisplaySituationPreview();
        DisplaySituationHappening();             
        serializedObject.ApplyModifiedProperties();
    }

    private void DisplaySituationDefinition()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Definition", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_situationTypeProperty);
        EditorGUILayout.PropertyField(_descriptionProperty);
        EditorGUILayout.PropertyField(_formatProperty, true);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void DisplaySituationPreview()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("ADU");
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void DisplaySituationHappening()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Happening", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        switch (_situation.Type)
        {
            case Situation.SituationType.TextInteractive:
                DisplayChoicesHappening();
                break;

            case Situation.SituationType.TextOnly:
                DisplaySituationHappening();
                break;

            default:
                throw new UnityException(_situation.Type + " not handled by SituationEditor script.");
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void DisplayChoicesHappening()
    {
        EditorGUILayout.PropertyField(_choicesProperty, true);
        _situation.NextSituation = null;

        if(GUILayout.Button("Create New Choice"))
        {
            _situation.AddChoice();
        }
    }

    private void DisplayNextSituationHappening()
    {
        EditorGUILayout.PropertyField(_nextSituationProperty);
        _situation.Choices = null;
    }
}
