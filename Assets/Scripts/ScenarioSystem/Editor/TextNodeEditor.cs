using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextNode))]
public class TextNodeEditor : Editor
{
    public enum EditorType
    {
        PlayerChoice, TextInput
    }

    public EditorType editorType;
    private TextNode textNode;
    private SerializedProperty textProperty;
    
    private const string textNodePropText = "text";

    private void OnEnable()
    {
        textNode = (TextNode)target;

        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }
        textProperty = serializedObject.FindProperty(textNodePropText);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.TextArea(textProperty.stringValue);

        if (GUILayout.Button("-"))
        {
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
    

    public static TextNode CreateTextNode()
    {
        TextNode newTextNode = CreateInstance<TextNode>();           
        newTextNode.text = "No text set.";
        return newTextNode;
    }

    public static TextNode CreateTextNode(string name)
    {
        TextNode newTextNode = CreateInstance<TextNode>();
        newTextNode.text = "No text set.";
        return newTextNode;
    }

}
