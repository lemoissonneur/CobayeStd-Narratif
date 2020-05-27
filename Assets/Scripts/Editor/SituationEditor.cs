using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Situation))]
public class SituationEditor : Editor
{
    private SerializedProperty _nameProperty;
    private SerializedProperty _situationTypeProperty;
    private SerializedProperty _descriptionProperty;
    private SerializedProperty _formatProperty;
    private SerializedProperty _nextSituationProperty;
    private SerializedProperty _choicesProperty;

    private const string situationPropName = "m_Name";
    private const string situationPropSituationType = "Type";
    private const string situationPropDescription = "Description";
    private const string situationPropFormat = "Format";
    private const string situationPropNextSituation = "NextSituation";
    private const string situationPropChoices = "Choices";

    private Situation _situation;
    private ChoiceEditor[] _choiceEditors;
    private string newChoiceName = "New Choice";

    internal Vector2 _scrollPos;

    private void OnEnable()
    {
        _situation = (Situation)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        _nameProperty = serializedObject.FindProperty(situationPropName);
        _situationTypeProperty = serializedObject.FindProperty(situationPropSituationType);
        _descriptionProperty = serializedObject.FindProperty(situationPropDescription);
        _formatProperty = serializedObject.FindProperty(situationPropFormat);
        _nextSituationProperty = serializedObject.FindProperty(situationPropNextSituation);
        _choicesProperty = serializedObject.FindProperty(situationPropChoices);

        if(_situation.Choices == null)
        {
            _situation.Choices = new Choice[0];
        }

        if(_choiceEditors == null)
        {
            CreateEditors();
        }
    }

    private void OnDisable()
    {
        foreach(var choiceEditor in _choiceEditors)
        {
            DestroyImmediate(choiceEditor);
        }

        _choiceEditors = null;
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
        EditorGUILayout.PropertyField(_nameProperty);
        EditorGUILayout.PropertyField(_situationTypeProperty);
        EditorGUILayout.PropertyField(_descriptionProperty);
        EditorGUILayout.PropertyField(_formatProperty);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void DisplaySituationPreview()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
        if (_situation.Format != null)
        {

            GUIStyle myTextArea = new GUIStyle(EditorStyles.textArea);
            myTextArea.wordWrap = true;
            myTextArea.font = _situation.Format.font;
            myTextArea.fontSize = _situation.Format.size;
            myTextArea.fontStyle = _situation.Format.style;
            myTextArea.normal.textColor = _situation.Format.textColor;

            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = _situation.Format.backColor;

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false);
            EditorGUILayout.SelectableLabel(_situation.Description, myTextArea, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
            GUI.backgroundColor = defaultColor;
        }
        else
        {
            EditorGUILayout.LabelField(new GUIContent("Please provide a text format to preview text."));
        }
        EditorGUILayout.EndVertical();
    }

    public static Situation CreateSituation(string name)
    {
        Situation newSituation = CreateInstance<Situation>();
        newSituation.name = name;
        return newSituation;
    }

    private void DisplaySituationHappening()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        switch (_situation.Type)
        {
            case Situation.SituationType.TextInteractive:
                DisplayChoicesHappening();
                break;

            case Situation.SituationType.TextOnly:
                DisplayNextSituationHappening();
                break;

            default:
                throw new UnityException(_situation.Type + " not handled by SituationEditor script.");
        }
        EditorGUILayout.EndVertical();
    }

    private void DisplayChoicesHappening()
    {
        EditorGUILayout.LabelField("Available Choices", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        _situation.NextSituation = null;

        if(_choiceEditors.Length != TryGetChoicesLength())
        {
            foreach (var choiceEditor in _choiceEditors)
            {
                DestroyImmediate(choiceEditor);
            }

            CreateEditors();
        }

        for (int i = 0 ; i < _situation.Choices.Length; i++)
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUI.indentLevel++;

            //_choiceEditors[i].OnInspectorGUI();
            EditorGUILayout.PropertyField(_choicesProperty.GetArrayElementAtIndex(i), new GUIContent("Choice n°" + (i+1).ToString()));
            if (GUILayout.Button("-"))
            {
                RemoveChoice((Choice)_choiceEditors[i].target);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        newChoiceName = EditorGUILayout.TextField(GUIContent.none, newChoiceName);
        if(GUILayout.Button("+"))
        {
            AddChoice(newChoiceName);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;
    }

    private void DisplayNextSituationHappening()
    {
        EditorGUILayout.LabelField("Next Situation", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_nextSituationProperty);
        _situation.Choices = new Choice[0];
        EditorGUI.indentLevel--;
    }

    private void CreateEditors()
    {
        _choiceEditors = new ChoiceEditor[_situation.Choices.Length];

        for(int i = 0; i < _choiceEditors.Length; i++)
        {
            var choice = TryGetChoiceAt(i);
            _choiceEditors[i] = CreateEditor(choice) as ChoiceEditor;          
        }
    }

    private void AddChoice(string name)
    {
        Choice newChoice = ChoiceEditor.CreateChoice(name);

        Undo.RecordObject(newChoice, "Created new Choice");
        AssetDatabase.AddObjectToAsset(newChoice, _situation);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newChoice));
        ArrayUtility.Add(ref _situation.Choices, newChoice);
        EditorUtility.SetDirty(_situation);
    }

    private void RemoveChoice(Choice choice)
    {
        Undo.RecordObject(_situation, "Removing choice");
        ArrayUtility.Remove(ref _situation.Choices, choice);
        DestroyImmediate(choice, true);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(_situation);
    }

    public Choice TryGetChoiceAt(int index)
    {
        if (_situation.Choices == null || _situation.Choices[0] == null)
        {
            return null;
        }

        if (index >= _situation.Choices.Length)
        {
            return _situation.Choices[0];
        }

        return _situation.Choices[index];
    }

    public int TryGetChoicesLength()
    {
        if(_situation.Choices == null)
        {
            return 0;
        }

        return _situation.Choices.Length;
    }
}
