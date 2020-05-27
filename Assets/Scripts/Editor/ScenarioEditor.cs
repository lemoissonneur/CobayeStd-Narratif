using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Scenario))]
public class ScenarioEditor : Editor
{
    private SerializedProperty _situationsProperty;

    private const string scenarioPropSituation = "Situations";

    private Scenario _scenario;
    private SituationEditor[] _situationEditors;
    private string newSituationName = "New Situation";

    private void OnEnable()
    {
        _scenario = (Scenario)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        _situationsProperty = serializedObject.FindProperty(scenarioPropSituation);

        if (_scenario.Situations == null)
        {
            _scenario.Situations = new Situation[0];
        }

        if (_situationEditors == null)
        {
            CreateEditors();
        }
    }

    private void OnDisable()
    {
        foreach (var situationEditor in _situationEditors)
        {
            DestroyImmediate(situationEditor);
        }

        _situationEditors = null;
    }

    private void CreateEditors()
    {
        _situationEditors = new SituationEditor[_scenario.Situations.Length];

        for (int i = 0; i < _situationEditors.Length; i++)
        {
            var situation = TryGetSituationAt(i);
            _situationEditors[i] = CreateEditor(situation) as SituationEditor;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Situations", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        if (_situationEditors.Length != TryGetSituationsLength())
        {
            foreach (var situationEditor in _situationEditors)
            {
                DestroyImmediate(situationEditor);
            }

            CreateEditors();
        }

        for (int i = 0; i < _scenario.Situations.Length; i++)
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_situationsProperty.GetArrayElementAtIndex(i), new GUIContent("Situation n°" + (i + 1).ToString()));
            if (GUILayout.Button("-"))
            {
                RemoveSituation((Situation)_situationEditors[i].target);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        newSituationName = EditorGUILayout.TextField(GUIContent.none, newSituationName);
        if (GUILayout.Button("+"))
        {
            AddSituation(newSituationName);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void AddSituation(string name)
    {
        Situation newSituation = SituationEditor.CreateSituation(name);

        Undo.RecordObject(newSituation, "Created new Situation");
        AssetDatabase.AddObjectToAsset(newSituation, _scenario);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newSituation));
        ArrayUtility.Add(ref _scenario.Situations, newSituation);
        EditorUtility.SetDirty(_scenario);
    }

    private void RemoveSituation(Situation situation)
    {
        Undo.RecordObject(_scenario, "Removing situation");
        ArrayUtility.Remove(ref _scenario.Situations, situation);
        DestroyImmediate(situation, true);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(_scenario);
    }

    public Situation TryGetSituationAt(int index)
    {
        if (_scenario.Situations == null || _scenario.Situations[0] == null)
        {
            return null;
        }

        if (index >= _scenario.Situations.Length)
        {
            return _scenario.Situations[0];
        }

        return _scenario.Situations[index];
    }

    public int TryGetSituationsLength()
    {
        if (_scenario.Situations == null)
        {
            return 0;
        }

        return _scenario.Situations.Length;
    }
}
