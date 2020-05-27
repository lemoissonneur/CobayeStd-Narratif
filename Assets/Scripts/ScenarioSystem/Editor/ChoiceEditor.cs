using UnityEditor;

[CustomEditor(typeof(Choice))]
public class ChoiceEditor : Editor
{
    private Choice _choice;

    private void OnEnable()
    {
        _choice = (Choice)target;
        
        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }        
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        //EditorGUILayout.PropertyField();
        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();

    }

    public static Choice CreateChoice(string name)
    {
        Choice newChoice = CreateInstance<Choice>();
        newChoice.name = name;
        return newChoice;
    }
}
