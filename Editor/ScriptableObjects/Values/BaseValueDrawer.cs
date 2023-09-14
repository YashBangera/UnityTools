using UnityEditor;
using UnityEngine;
using UnityTools.ScriptableObjects;
using System.Collections.Generic;
using System.Reflection;

namespace UnityTools.ScriptableObjects.Editor
{
	[CustomPropertyDrawer(typeof(NonGenericBaseValueSO), true)]
	public class BaseValueDrawer : CreatableFoldoutScriptableObjectDrawer
	{

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			base.OnGUI(position, property, label);

			if (foldoutStates[instanceID])
			{
				SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);

				SerializedProperty defaultValueProperty = serializedObject.FindProperty("_defaultValue");
				SerializedProperty isPersistentProperty = serializedObject.FindProperty("_isPersistent");
				SerializedProperty currentValueProperty = serializedObject.FindProperty("_currentValue");
				SerializedProperty onValueChangedProperty = serializedObject.FindProperty("_onValueChanged");

				// Display fields within the foldout section
				position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
				position.height = EditorGUIUtility.singleLineHeight;

				EditorGUI.PropertyField(position, defaultValueProperty, new GUIContent("Default Value"));
				position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

				EditorGUI.PropertyField(position, isPersistentProperty, new GUIContent("Is Persistent"));
				position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

				EditorGUI.BeginChangeCheck();
				EditorGUI.BeginDisabledGroup(isPersistentProperty.boolValue);
				EditorGUI.PropertyField(position, currentValueProperty, new GUIContent("Current Value"));
				position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
				EditorGUI.EndDisabledGroup();

				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
				}

				// Adjust the position height for UnityEvent
				position.height = EditorGUIUtility.singleLineHeight * 5; // adjust as per your setting in GetPropertyHeight
				EditorGUI.PropertyField(position, onValueChangedProperty, new GUIContent("On Value Changed"), true);
				position.y += position.height + EditorGUIUtility.standardVerticalSpacing;

				if (isPersistentProperty.boolValue)
				{
					EditorGUI.HelpBox(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight * 2), "Persistent values cannot be changed at runtime.", MessageType.Info);
				}

				serializedObject.ApplyModifiedProperties();
			}

		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			int instanceID = property.serializedObject.targetObject.GetInstanceID();

			// If it's not expanded or no object exists, only return the height of a single line.
			bool objectExists = property.objectReferenceValue != null;
			if (!foldoutStates.ContainsKey(instanceID) || !foldoutStates[instanceID] || !objectExists)
				return EditorGUIUtility.singleLineHeight;

			// Base height for a single property
			float baseHeight = EditorGUIUtility.singleLineHeight;
			float totalHeight = baseHeight; // For the main foldout line

			// Add height for each property you're drawing within the foldout section
			totalHeight += baseHeight; // Default Value
			totalHeight += baseHeight; // Is Persistent
			totalHeight += baseHeight; // Current Value

			int listenersCount = 0;

			if (objectExists)
			{
				SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
				SerializedProperty onValueChangedProperty = serializedObject.FindProperty("_onValueChanged");

				if (onValueChangedProperty != null)
				{
					SerializedProperty listeners = onValueChangedProperty.FindPropertyRelative("m_PersistentCalls.m_Calls");
					listenersCount = listeners.arraySize;
				}
			}

			// Add height for UnityEvent; start with 3 lines for base elements, then add for listeners.
			totalHeight += baseHeight * (6 + listenersCount * 2);

			// Add spaces between properties
			totalHeight += EditorGUIUtility.standardVerticalSpacing * 4;

			if (objectExists)
			{
				SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
				SerializedProperty isPersistentProperty = serializedObject.FindProperty("_isPersistent");
				if (isPersistentProperty != null && isPersistentProperty.boolValue)
				{
					totalHeight += baseHeight * 2; // Adjust height for HelpBox, multiplying by 2 as an approximation
				}
			}

			return totalHeight;
		}

	}
}
