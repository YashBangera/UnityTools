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

		protected override void OnEnable()
		{
			localizedStringProp = serializedObject.FindProperty("m_localizedString");
			currentLocaleProp = serializedObject.FindProperty("m_currentLocale");
			EditorApplication.update += UpdateLocalizedText;

			base.OnEnable();
		}

		protected override void OnDisable()
		{
			EditorApplication.update -= UpdateLocalizedText;
			base.OnDisable();
		}

		private void UpdateLocalizedText()
		{
			var targetComponent = (LocalizedTextMeshProUGUI)target;
			targetComponent.RefreshLocalizedText();
			Repaint();
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
				UpdateLocalizedText(); // Force update
			}

			// If localization is not set, draw the main settings which includes the text field
			if (localizedStringProp.objectReferenceValue == null)
			{
				base.OnInspectorGUI();
			}
			else
			{
				SerializedObject localizedStringSO = new SerializedObject(localizedStringProp.objectReferenceValue);

				EditorGUI.BeginChangeCheck(); // Check for changes

				// Display the m_currentLocale as a read-only field
				if (currentLocaleProp != null)
				{
					EditorGUILayout.LabelField("Current Locale", currentLocaleProp.enumNames[currentLocaleProp.enumValueIndex]);
				}
				else
				{
					EditorGUILayout.LabelField("Error", "Cannot find the m_currentLocale property.");
				}

				SerializedProperty pairs = localizedStringSO.FindProperty("m_localizedStrings");

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

				DrawMainSettings();
				DrawExtraSettings();

				// If there were changes, apply them and refresh the text
				if (EditorGUI.EndChangeCheck())
				{
					localizedStringSO.ApplyModifiedProperties();
					UpdateLocalizedText(); // Force update
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
