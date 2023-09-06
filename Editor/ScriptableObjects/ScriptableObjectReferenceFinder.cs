using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditorInternal;

namespace UnityTools.ScriptableObjects.Editor
{
    public class ScriptableObjectReferenceFinder : EditorWindow
    {
        private ReorderableList scriptableObjectList;
        private List<ReferenceInfo> referencedComponents = new List<ReferenceInfo>();
        private List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();
        private int selectedScriptableObjectIndex = -1;

        [System.Serializable]
        private class ReferenceInfo
        {
            public ScriptableObject scriptableObject;
            public GameObject gameObject;
            public Component component;
        }

        [MenuItem("Window/Scriptable Object Reference Finder")]
        public static void ShowWindow()
        {
            GetWindow<ScriptableObjectReferenceFinder>("Scriptable Object Reference Finder");
        }

        [MenuItem("Assets/Find References to Scriptable Object")]
        public static void FindReferencesToSelectedScriptableObject()
        {
            ScriptableObject selectedScriptableObject = Selection.activeObject as ScriptableObject;
            if (selectedScriptableObject != null)
            {
                ScriptableObjectReferenceFinder window = GetWindow<ScriptableObjectReferenceFinder>("Scriptable Object Reference Finder");
                window.FindReferences(window.scriptableObjects.IndexOf(selectedScriptableObject));
            }
        }

        private void OnEnable()
        {
            FindScriptableObjects();
            CreateReorderableList();
        }

        private void OnGUI()
        {
            GUILayout.Label("References to Scriptable Objects", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (scriptableObjectList != null)
            {
                scriptableObjectList.DoLayoutList();
            }

            if (selectedScriptableObjectIndex >= 0 && selectedScriptableObjectIndex < scriptableObjects.Count)
            {
                ScriptableObject selectedScriptableObject = scriptableObjects[selectedScriptableObjectIndex];

                GUILayout.Label("References to Selected Scriptable Object:", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Scriptable Object:", selectedScriptableObject.name);

                foreach (ReferenceInfo referenceInfo in referencedComponents)
                {
                    if (referenceInfo.scriptableObject == selectedScriptableObject)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.ObjectField(referenceInfo.gameObject, typeof(GameObject), true);
                        EditorGUILayout.ObjectField(referenceInfo.component, typeof(Component), true);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }

        private void CreateReorderableList()
        {
            scriptableObjectList = new ReorderableList(scriptableObjects, typeof(ScriptableObject), true, true, true, true);

            scriptableObjectList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "All Scriptable Objects");
            };

            scriptableObjectList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (index < scriptableObjects.Count)
                {
                    Rect objectFieldRect = new Rect(rect.x, rect.y, rect.width - 110, EditorGUIUtility.singleLineHeight);
                    EditorGUI.BeginChangeCheck();
                    ScriptableObject newScriptableObject = (ScriptableObject)EditorGUI.ObjectField(
                        objectFieldRect, scriptableObjects[index], typeof(ScriptableObject), false);

                    if (EditorGUI.EndChangeCheck() && newScriptableObject != null)
                    {
                        EditorGUIUtility.PingObject(newScriptableObject);
                        selectedScriptableObjectIndex = index;
                    }

                    // Create a button to find references
                    Rect findReferencesButtonRect = new Rect(rect.x + rect.width - 110, rect.y, 110, EditorGUIUtility.singleLineHeight);
                    if (GUI.Button(findReferencesButtonRect, "Find References"))
                    {
                        selectedScriptableObjectIndex = index;
                        FindReferences(index);
                    }
                }
            };

            scriptableObjectList.onSelectCallback = (ReorderableList list) =>
            {
                // You can keep this empty as we handle the selection in the "Find References" button
            };
        }

        private void FindScriptableObjects()
        {
            scriptableObjects.Clear();

            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // Check if the asset is located in a package
                if (!assetPath.Contains("Packages"))
                {
                    ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                    if (scriptableObject != null)
                    {
                        scriptableObjects.Add(scriptableObject);
                    }
                }
            }
        }

        private void FindReferences(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < scriptableObjects.Count)
            {
                ScriptableObject selectedScriptableObject = scriptableObjects[selectedIndex];
                referencedComponents.Clear();

                // Find references in scenes
                FindReferencesInScenes(selectedScriptableObject);

                // Find references in prefabs
                FindReferencesInPrefabs(selectedScriptableObject);

                Repaint();
            }
        }

        private void FindReferencesInScenes(ScriptableObject selectedScriptableObject)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.isLoaded)
                {
                    GameObject[] rootGameObjects = scene.GetRootGameObjects();

                    foreach (GameObject gameObject in rootGameObjects)
                    {
                        Component[] components = gameObject.GetComponentsInChildren<Component>(true);

                        foreach (Component component in components)
                        {
                            if (component == null)
                            {
                                continue;
                            }

                            CheckAndAddReference(selectedScriptableObject, component);
                        }
                    }
                }
            }
        }

        private void FindReferencesInPrefabs(ScriptableObject selectedScriptableObject)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (string guid in guids)
            {
                string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                if (prefab != null)
                {
                    Component[] components = prefab.GetComponentsInChildren<Component>(true);

                    foreach (Component component in components)
                    {
                        if (component == null)
                        {
                            continue;
                        }

                        CheckAndAddReference(selectedScriptableObject, component);
                    }
                }
            }
        }

        private void CheckAndAddReference(ScriptableObject selectedScriptableObject, Component component)
        {
            SerializedObject serializedObject = new SerializedObject(component);
            SerializedProperty iterator = serializedObject.GetIterator();

            while (iterator.NextVisible(true))
            {
                if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                {
                    if (iterator.objectReferenceValue != null)
                    {
                        if (iterator.objectReferenceValue == selectedScriptableObject)
                        {
                            referencedComponents.Add(new ReferenceInfo
                            {
                                scriptableObject = selectedScriptableObject,
                                gameObject = component.gameObject,
                                component = component
                            });
                            break;
                        }
                    }
                }
            }
        }
    }
}
