using UnityEditor;
using TMPro;
using UnityTools.ScriptableObjects;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
    [CustomEditor(typeof(LocalizedTextMeshProUGUI), true), CanEditMultipleObjects]
    public class LocalizedTextMeshProUGUIEditor : TMPro.EditorUtilities.TMP_EditorPanelUI
    {
        SerializedProperty localizedStringProp;
        SerializedProperty currentLocaleProp;

        bool showDetails = false;

        protected override void OnEnable()
        {
            localizedStringProp = serializedObject.FindProperty("m_localizedString");
            currentLocaleProp = serializedObject.FindProperty("m_currentLocale");
            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw the LocalizedString field first
            Object currentLocalizedString = localizedStringProp.objectReferenceValue;
            Object newLocalizedString = EditorGUILayout.ObjectField("Localized String", currentLocalizedString, typeof(LocalizedString), false);

            if (newLocalizedString != currentLocalizedString)
            {
                localizedStringProp.objectReferenceValue = newLocalizedString;
                serializedObject.ApplyModifiedProperties(); // Ensure we immediately apply this change
            }


            // If localization is not set, draw the main settings which includes the text field
            if (localizedStringProp.objectReferenceValue == null)
            {
                base.OnInspectorGUI(); // This will draw everything from the original TMP_EditorPanelUI
            }
            else
            {
                // Now draw the localization pairs, like before
                if (localizedStringProp.objectReferenceValue != null)
                {
                    SerializedObject localizedStringSO = new SerializedObject(localizedStringProp.objectReferenceValue);
                    SerializedProperty pairs = localizedStringSO.FindProperty("m_localizedStrings");
                    // Display the m_currentLocale as a read-only field
                    if (currentLocaleProp != null)
                    {
                        EditorGUILayout.LabelField("Current Locale", currentLocaleProp.enumNames[currentLocaleProp.enumValueIndex]);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("Error", "Cannot find the m_currentLocale property.");
                    }


                    for (int i = 0; i < pairs.arraySize; i++)
                    {
                        SerializedProperty pair = pairs.GetArrayElementAtIndex(i);
                        SerializedProperty locale = pair.FindPropertyRelative("locale");
                        SerializedProperty localizedStr = pair.FindPropertyRelative("localizedString");

                        EditorGUILayout.PropertyField(locale);
                        EditorGUILayout.PropertyField(localizedStr);

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Remove"))
                        {
                            pairs.DeleteArrayElementAtIndex(i);
                            break;  // Exit the loop, as the array content has changed
                        }
                        GUILayout.EndHorizontal();
                    }

                    if (GUILayout.Button("Add New Pair"))
                    {
                        pairs.InsertArrayElementAtIndex(pairs.arraySize);
                    }

                    localizedStringSO.ApplyModifiedProperties();


                    // If localization is set, manually draw all other components excluding the text field

                    DrawMainSettings();

                    DrawExtraSettings();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }


    }
}
