using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
	[CustomEditor(typeof(BaseValueSO<>), true)]
	public class BaseValueSOInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			SerializedProperty defaultValueProperty = serializedObject.FindProperty("_defaultValue");
			SerializedProperty isPersistentProperty = serializedObject.FindProperty("_isPersistent");
			SerializedProperty onCollectionChanged = serializedObject.FindProperty("_onValueChanged");

			// Display the default value and isPersistent fields.
			EditorGUILayout.PropertyField(defaultValueProperty, new GUIContent("Default Value"));
			EditorGUILayout.PropertyField(isPersistentProperty, new GUIContent("Is Persistent"));


			// Display the currentValue based on its actual type.
			SerializedProperty currentValueProperty = serializedObject.FindProperty("_currentValue");

			EditorGUI.BeginChangeCheck();

			if (currentValueProperty.propertyType == SerializedPropertyType.Generic)
			{
				EditorGUILayout.LabelField("Current Value", "Not Supported");
			}
			else
			{
				EditorGUI.BeginDisabledGroup(isPersistentProperty.boolValue); // Disable if isPersistent is true
				EditorGUILayout.PropertyField(currentValueProperty, new GUIContent("Current Value"));
				EditorGUI.EndDisabledGroup();
			}

			EditorGUILayout.PropertyField(onCollectionChanged, new GUIContent("On Value Changed"));


			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}

			if (isPersistentProperty.boolValue)
			{
				EditorGUILayout.HelpBox("Persistent values cannot be changed at runtime.", MessageType.Info);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
