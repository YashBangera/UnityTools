using UnityEditor;
using TMPro;
using UnityTools.ScriptableObjects;
using UnityEngine;

[CustomEditor(typeof(LocalizedTextMeshProUGUI), true), CanEditMultipleObjects]
public class LocalizedTextMeshProUGUIEditor : TMPro.EditorUtilities.TMP_EditorPanelUI
{
    SerializedProperty localizedStringProp;

    protected override void OnEnable()
    {
        // Get the localizedString property
        localizedStringProp = serializedObject.FindProperty("m_localizedString");

        // Call the base OnEnable to ensure TMP's properties are also initialized
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        // Draw the localizedString field
        EditorGUILayout.PropertyField(localizedStringProp, new GUIContent("Localized String"));
        // Draw the TMP inspector first
        base.OnInspectorGUI();
        // Apply changes to the serializedProperty
        serializedObject.ApplyModifiedProperties();
    }
}
