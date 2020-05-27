using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Choice))]
public class ChoiceEditor : Editor
{
    private SerializedProperty _nameProperty;
    private SerializedProperty _keyStringProperty;
    private SerializedProperty _descriptionProperty;
    private SerializedProperty _formatProperty;
    private SerializedProperty _nextSituationProperty;

    private const string choicePropName = "m_Name";
    private const string choicePropKeyString = "KeyString";
    private const string choicePropDescription = "Description";
    private const string choicePropFormat = "Format";
    private const string choicePropNextSituation = "NextSituation";
    
    private Choice _choice;
    internal Vector2 _scrollPos;

    private void OnEnable()
    {
        _choice = (Choice)target;
        
        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }

        _nameProperty = serializedObject.FindProperty(choicePropName);
        _keyStringProperty = serializedObject.FindProperty(choicePropKeyString);
        _descriptionProperty = serializedObject.FindProperty(choicePropDescription);
        _formatProperty = serializedObject.FindProperty(choicePropFormat);
        _nextSituationProperty = serializedObject.FindProperty(choicePropNextSituation);
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();

        DisplayDefinition();
        DisplayPreview();
        DisplayNextSituation();

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayDefinition()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Definition", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_nameProperty);
        EditorGUILayout.PropertyField(_keyStringProperty);
        EditorGUILayout.PropertyField(_descriptionProperty);
        EditorGUILayout.PropertyField(_formatProperty);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void DisplayPreview()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
        if (_choice.Format != null)
        {
            GUIStyle myTextArea = new GUIStyle(EditorStyles.textArea);
            myTextArea.wordWrap = true;
            myTextArea.font = _choice.Format.font;
            myTextArea.fontSize = _choice.Format.size;
            myTextArea.fontStyle = _choice.Format.style;
            myTextArea.normal.textColor = _choice.Format.textColor;

            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = _choice.Format.backColor;

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false);
            EditorGUILayout.SelectableLabel(_choice.Description, myTextArea, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
            GUI.backgroundColor = defaultColor;
        }
        else
        {
            EditorGUILayout.LabelField(new GUIContent("Please provide a text format to preview text."));
        }
        EditorGUILayout.EndVertical();
    }

    private void DisplayNextSituation()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Next Situation", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_nextSituationProperty);        
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    public static Choice CreateChoice(string name)
    {
        Choice newChoice = CreateInstance<Choice>();
        newChoice.name = name;
        return newChoice;
    }
}
