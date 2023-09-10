using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.ScriptableObjects
{
	public class CreateLocalizedButtonWizard : ScriptableWizard
	{
		private static Transform parentTransform; // Store the parent transform if selected in the Hierarchy

		[MenuItem("GameObject/UI/Localized Button", false, 11)] // 11 for context menu order
		public static void CreateLocalizedButton(MenuCommand menuCommand)
		{
			parentTransform = Selection.activeTransform; // Get the selected GameObject (if any)

			// Create a new instance of the Button GameObject
			var buttonObject = new GameObject("LocalizedButton");
			var buttonRectTransform = buttonObject.AddComponent<RectTransform>();
			buttonRectTransform.sizeDelta = new Vector2(200f, 100f); // Set the button size

			// Create a new instance of the Button component
			var imageComponent = buttonObject.AddComponent<Image>();
			var buttonComponent = buttonObject.AddComponent<Button>();
			buttonComponent.targetGraphic = imageComponent;

			// Create a new instance of the Text GameObject as a child of the button
			var textObject = new GameObject("Text");
			var textRectTransform = textObject.AddComponent<RectTransform>();
			textRectTransform.SetParent(buttonObject.transform, false);

			// Add a LocalizedTextMeshProUGUI component to the Text GameObject
			var localizedText = textObject.AddComponent<LocalizedTextMeshProUGUI>();
			localizedText.text = "Button";

			// Set the text color to black
			localizedText.color = Color.black;

			// Set the alignment of the text to center
			localizedText.alignment = TMPro.TextAlignmentOptions.Center;

			// Record the changes and set the parent if a parent is selected
			Undo.RecordObject(buttonObject.transform, "Create Localized Button");
			if (parentTransform != null)
			{
				buttonObject.transform.SetParent(parentTransform, false);
			}

			// Mark the object as dirty to save the changes
			EditorUtility.SetDirty(buttonObject);

			// Focus on the newly created GameObject in the Hierarchy
			Selection.activeGameObject = buttonObject;

			// Select the newly created GO in the Project view
			Selection.activeObject = localizedText.gameObject;
		}
	}
}
