using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
	[CustomEditor(typeof(BaseCollectionSO<>), true)]
	public class BaseCollectionSOInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			SerializedProperty defaultCollectionProperty = serializedObject.FindProperty("_defaultCollection");
			SerializedProperty isPersistentProperty = serializedObject.FindProperty("_isPersistent");
			SerializedProperty onCollectionChanged = serializedObject.FindProperty("_onCollectionChanged");

			// Display the default collection and isPersistent fields.
			EditorGUILayout.PropertyField(defaultCollectionProperty, new GUIContent("Default Collection"));
			EditorGUILayout.PropertyField(isPersistentProperty, new GUIContent("Is Persistent"));

			if (isPersistentProperty.boolValue)
			{
				EditorGUILayout.HelpBox("Persistent collections cannot be changed at runtime.", MessageType.Info);
			}

			// Display the currentCollection.
			SerializedProperty currentCollectionProperty = serializedObject.FindProperty("_currentCollection");

			EditorGUI.BeginChangeCheck();

			EditorGUI.BeginDisabledGroup(isPersistentProperty.boolValue); // Disable if isPersistent is true
			EditorGUILayout.PropertyField(currentCollectionProperty, new GUIContent("Current Collection"), true);
			EditorGUI.EndDisabledGroup();


			EditorGUILayout.PropertyField(onCollectionChanged, new GUIContent("On Collection Changed"));

			
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
