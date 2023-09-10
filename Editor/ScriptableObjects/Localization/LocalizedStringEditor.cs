using UnityEditor;
using UnityEngine;
using UnityTools.ScriptableObjects;

namespace UnityTools.ScriptableObjects.Editor
{

    [CustomEditor(typeof(LocalizedString))]
    public class LocalizedStringEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            base.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck())
            {
                LocalizedString changedLocalizedString = (LocalizedString)target;

                // Find all objects of type LocalizedTextMeshProUGUI in the scene
                LocalizedTextMeshProUGUI[] allLocalizedTexts = GameObject.FindObjectsOfType<LocalizedTextMeshProUGUI>();

                foreach (var localizedText in allLocalizedTexts)
                {
                    if (localizedText.ReferencedLocalizedString == changedLocalizedString)
                    {
                        localizedText.RefreshLocalizedText();
                    }
                }
            }
        }
    }
}
