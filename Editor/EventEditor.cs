using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework.Editor
{
    [CustomEditor(typeof(Event))]
    public class EventEditor : UnityEditor.Editor
    {
        private SerializedProperty channel;
        private SerializedProperty onEventRaised;

        private void OnEnable()
        {
            channel = serializedObject.FindProperty("channel");
            onEventRaised = serializedObject.FindProperty("OnEventRaised");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(channel);
            EditorGUILayout.PropertyField(onEventRaised);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
