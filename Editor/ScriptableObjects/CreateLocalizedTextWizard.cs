using UnityEditor;
using UnityEngine;
using UnityTools.ScriptableObjects;

namespace UnityTools.ScriptableObjects
{
	public class CreateLocalizedTextWizard : ScriptableWizard
	{
		private static Transform parentTransform; // Store the parent transform if selected in the Hierarchy

		[MenuItem("GameObject/UI/Localized Text", false, 11)] // 11 for context menu order
		public static void CreateLocalizedText(MenuCommand menuCommand)
		{
			parentTransform = Selection.activeTransform; // Get the selected GameObject (if any)

			// Create a new instance of the LocalizedTextMeshProUGUI script
			var textObject = new GameObject("LocalizedTextMeshProUGUI");

			// Create a new instance of the LocalizedString scriptable object
			var localizedString = ScriptableObject.CreateInstance<LocalizedString>();
			localizedString.name = "NewLocalizedString";

			// Specify the path where the SO is created
			string assetPath = EditorUtility.SaveFilePanelInProject("Save Localized String", "NewLocalizedString", "asset", "Please enter a name for the Localized String");

			if (!string.IsNullOrEmpty(assetPath))
			{
				AssetDatabase.CreateAsset(localizedString, assetPath);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();

				// Assign the LocalizedString to the LocalizedTextMeshProUGUI
				var localizedText = textObject.AddComponent<LocalizedTextMeshProUGUI>();
				localizedText.SetLocalizedString(localizedString);
			}

			// Record the changes and set the parent if a parent is selected
			Undo.RecordObject(textObject.transform, "Create Localized Text");
			if (parentTransform != null)
			{
				textObject.transform.SetParent(parentTransform, false);
			}

			// Mark the object as dirty to save the changes
			EditorUtility.SetDirty(textObject);

			// Focus on the newly created GameObject in the Hierarchy
			Selection.activeGameObject = textObject;

			// Select the newly created ScriptableObject in the Project view
			Selection.activeObject = localizedString;
		}
	}
}
