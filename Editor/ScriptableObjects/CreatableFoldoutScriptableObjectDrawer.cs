using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace UnityTools.ScriptableObjects.Editor
{
    public class CreatableFoldoutScriptableObjectDrawer : PropertyDrawer
    {
        protected static Dictionary<int, bool> foldoutStates = new Dictionary<int, bool>();
		protected int instanceID = -1;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			instanceID = property.serializedObject.targetObject.GetInstanceID();

			// Ensure we have a state for this instance ID
			if (!foldoutStates.ContainsKey(instanceID))
				foldoutStates[instanceID] = false;

			bool objectExists = property.objectReferenceValue != null;

			// Adjusted to allow space for the label
			Rect objectFieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);

			EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), label);

			if (objectExists)
			{
				foldoutStates[instanceID] = EditorGUI.Foldout(new Rect(objectFieldRect.x, objectFieldRect.y, 15f, EditorGUIUtility.singleLineHeight), foldoutStates[instanceID], GUIContent.none, true);
				EditorGUI.ObjectField(new Rect(objectFieldRect.x + 20f, objectFieldRect.y, objectFieldRect.width - 20f, EditorGUIUtility.singleLineHeight), property, GUIContent.none);
			}
			else
			{
				Rect createButtonRect = new Rect(objectFieldRect.x, objectFieldRect.y, 80f, EditorGUIUtility.singleLineHeight);

				if (GUI.Button(createButtonRect, "Create"))
				{
					System.Type type = null;

					// Get the serialized object containing the field
					SerializedObject serializedObject = property.serializedObject;

					// Get the target object of the serialized object
					UnityEngine.Object targetObject = serializedObject.targetObject;

					if (targetObject != null)
					{
						// Use reflection to access the field
						FieldInfo fieldInfo = targetObject.GetType()
							.GetField(property.propertyPath, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

						if (fieldInfo != null)
						{
							// Get the generic argument type of the field (e.g., FloatValueSO)
							type = fieldInfo.FieldType;
						}
					}

					if (type != null)
					{
						// Default file name based on type name
						string fileName = type.Name;

						// Default folder path (you can customize this)
						string defaultFolderPath = "Assets/";

						// Construct a default file path
						string defaultFilePath = defaultFolderPath + fileName + ".asset";

						// Open a file save panel with a default path and title
						string filePath = EditorUtility.SaveFilePanel("Save ScriptableObject", defaultFolderPath, fileName, "asset");

						if (!string.IsNullOrEmpty(filePath))
						{
							// Make sure the selected path is within the Assets folder
							if (filePath.StartsWith(Application.dataPath))
							{
								// Convert the absolute path to a relative path within the Assets folder
								filePath = "Assets" + filePath.Substring(Application.dataPath.Length);

								ScriptableObject newSO = ScriptableObject.CreateInstance(type);
								AssetDatabase.CreateAsset(newSO, filePath);
								AssetDatabase.SaveAssets();

								property.objectReferenceValue = newSO;
								serializedObject.ApplyModifiedProperties();
							}
							else
							{
								Debug.LogError("Selected path is not within the Assets folder.");
							}
						}
					}
					else
					{
						Debug.LogError("Field is not a valid type.");
					}
				}


				EditorGUI.ObjectField(new Rect(objectFieldRect.x + 85f, objectFieldRect.y, objectFieldRect.width - 85f, EditorGUIUtility.singleLineHeight), property, GUIContent.none);
			}
		}
	}
}
