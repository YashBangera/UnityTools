using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjects.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class ScriptableObjectFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(ScriptableObject), false);

            if (obj != property.objectReferenceValue)
            {
                property.objectReferenceValue = obj;
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }
    }
}
