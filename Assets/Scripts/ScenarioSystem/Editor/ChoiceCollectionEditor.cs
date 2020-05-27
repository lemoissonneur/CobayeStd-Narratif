using UnityEditor;

[CustomEditor(typeof(ChoiceCollection))]
public class ChoiceCollectionEditor : EditorWithSubEditors<ChoiceEditor, Choice>
{
    public SerializedProperty collectionsProperty;

    private SerializedProperty choicesProperty;
    private ChoiceCollection choiceCollection;

    private const string choiceCollectionPropAvailableChoicesName = "AvailableChoices";

    private void OnEnable()
    {
        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }

        choicesProperty = serializedObject.FindProperty(choiceCollectionPropAvailableChoicesName);

        choiceCollection = (ChoiceCollection)target;
        CheckAndCreateSubEditors(choiceCollection.AvailableChoices);
    }

    private void OnDisable()
    {
        CleanupEditors();
    }
    
    protected override void SubEditorSetup(ChoiceEditor editor)
    {
        // Nothing
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
