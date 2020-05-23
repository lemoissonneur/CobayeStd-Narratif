using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Scenario))]
public class ScenarioEditor : Editor
{
    public string[] ScenarioNodeNames
    {
        get
        {
            if(scenarioNodeNames == null)
            {
                SetScenarioNodeNames();
            }
            return scenarioNodeNames;
        }
        private set { scenarioNodeNames = value; }
    }
    private static string[] scenarioNodeNames;

    private Scenario scenario;
    private TextNodeEditor[] textNodeEditors;

    private void OnEnable()
    {
        scenario = (Scenario)target;

        if(scenario.textNodes == null)
        {
            scenario.textNodes = new LinkedList<TextNode>();
        }

        if(textNodeEditors == null)
        {
            CreateEditors();
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i< textNodeEditors.Length; i++)
        {
            DestroyImmediate(textNodeEditors[i]);
        }

        textNodeEditors = null;
    }

    private void SetScenarioNodeNames()
    {
        ScenarioNodeNames = new string[TryGetTextNodesLength()];
        for(int i = 0; i < ScenarioNodeNames.Length; i++)
        {
            ScenarioNodeNames[i] = TryGetTextNodeAt(i).name;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Scenario"))
        {
            string path = EditorUtility.OpenFilePanel("Load a JSON formatted scenario", Application.dataPath + "/Scenarios", "json");
            if (path.Length != 0)
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    scenario = JsonUtility.FromJson<Scenario>(json);
                }
            }
        }
        if (GUILayout.Button("Save Scenario"))
        {
            string path = EditorUtility.SaveFilePanel("Save a scenario with JSON formatting", Application.dataPath + "/Scenarios", scenario.name, "json");
            if (path.Length != 0)
            {
                using (StreamWriter w = new StreamWriter(path))
                {                    
                    string json = JsonUtility.ToJson(scenario, true);
                    w.Write(json);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if(textNodeEditors.Length != TryGetTextNodesLength())
        {
            for(int i = 0; i< textNodeEditors.Length; i++)
            {
                DestroyImmediate(textNodeEditors[i]);
            }

            CreateEditors();
        }

        for(int i = 0; i< textNodeEditors.Length; i++)
        {
            textNodeEditors[i].OnInspectorGUI();
        }

        if (TryGetTextNodesLength() > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        
        if (GUILayout.Button("New Node"))
        {
            AddTextNode();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void CreateEditors()
    {
        textNodeEditors = new TextNodeEditor[TryGetTextNodesLength()];

        for(int i = 0; i < textNodeEditors.Length; i++)
        {
            TextNode textNode = TryGetTextNodeAt(i);

            textNodeEditors[i] = CreateEditor(textNode) as TextNodeEditor;
            textNodeEditors[i].editorType = TextNodeEditor.EditorType.TextInput;
        }
    }

    public void AddTextNode()
    {
        TextNode newTextNode = TextNodeEditor.CreateTextNode();
        Debug.Log(newTextNode);

        Undo.RecordObject(newTextNode, "Created new Text Node");

        AssetDatabase.AddObjectToAsset(newTextNode, scenario);

        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newTextNode));

        scenario.textNodes.AddLast(newTextNode);

        EditorUtility.SetDirty(scenario);

        SetScenarioNodeNames();
    }

    public TextNode TryGetTextNodeAt(int index)
    {
        TextNode[] textNodes = new TextNode[TryGetTextNodesLength()];
        scenario.textNodes.CopyTo(textNodes, 0);

        if(scenario == null || textNodes[0] == null)
        {
            return null;
        }

        if(index >= textNodes.Length)
        {
            return textNodes[0];
        }

        return textNodes[index];
    }
    
    public int TryGetTextNodesLength()
    {
        if(scenario.textNodes == null)
        {
            return 0;
        }
        return scenario.textNodes.Count;
    }
}
