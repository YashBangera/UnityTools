using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
    public class LocalizationEditorWindow : EditorWindow
    {
        Vector2 scrollPosition;
        List<LocalizedString> localizedStrings = new List<LocalizedString>();

        [MenuItem("Window/Localization Editor")]
        public static void ShowWindow()
        {
            GetWindow<LocalizationEditorWindow>("Localization Editor");
        }

        private void OnEnable()
        {
            RefreshLocalizationList();
        }

        void RefreshLocalizationList()
        {
            localizedStrings.Clear();

            string[] guids = AssetDatabase.FindAssets("t:LocalizedString"); //finds all assets that are of type LocalizedString

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                LocalizedString ls = AssetDatabase.LoadAssetAtPath<LocalizedString>(path);

                if (ls != null)
                {
                    localizedStrings.Add(ls);
                }
            }
        }

        void OnGUI()
        {
            if (GUILayout.Button("Refresh List"))
            {
                RefreshLocalizationList();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var localizedString in localizedStrings)
            {
                EditorGUILayout.BeginVertical("box");

                // Display a clickable reference to the ScriptableObject
                EditorGUI.BeginDisabledGroup(true);  // Disables editing of this field
                EditorGUILayout.ObjectField("ScriptableObject:", localizedString, typeof(LocalizedString), false);
                EditorGUI.EndDisabledGroup();

                SerializedObject serializedObject = new SerializedObject(localizedString);
                SerializedProperty pairs = serializedObject.FindProperty("m_localizedStrings");

                // Table headers (Locales)
                EditorGUILayout.BeginHorizontal();
                foreach (LocalizedString.SupportedLocale locale in Enum.GetValues(typeof(LocalizedString.SupportedLocale)))
                {
                    EditorGUILayout.LabelField(locale.ToString(), EditorStyles.boldLabel, GUILayout.Width(150));
                }
                EditorGUILayout.EndHorizontal();

                // Table content
                EditorGUILayout.BeginHorizontal();
                foreach (LocalizedString.SupportedLocale locale in Enum.GetValues(typeof(LocalizedString.SupportedLocale)))
                {
                    SerializedProperty matchingPair = null;
                    for (int i = 0; i < pairs.arraySize; i++)
                    {
                        SerializedProperty pair = pairs.GetArrayElementAtIndex(i);
                        if (pair.FindPropertyRelative("locale").enumValueIndex == (int)locale)
                        {
                            matchingPair = pair;
                            break;
                        }
                    }

                    if (matchingPair != null)
                    {
                        EditorGUILayout.PropertyField(matchingPair.FindPropertyRelative("localizedString"), GUIContent.none, GUILayout.Width(150));
                    }
                    else
                    {
                        EditorGUILayout.LabelField("-", GUILayout.Width(150));
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (serializedObject.hasModifiedProperties)
                {
                    serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(localizedString);
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
        }











    }
}



