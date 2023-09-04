using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework.Editor
{
    [CustomPropertyDrawer(typeof(Event))]
    public class EventDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate the height for each row (label + object field)
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float totalHeight = lineHeight + spacing;

            // Split the rect into label, object field, and button portions
            Rect labelRect = new Rect(position.x, position.y, position.width, lineHeight);
            Rect objectFieldRect = new Rect(position.x, position.y + lineHeight + spacing, position.width - 100, lineHeight); // Adjusted width for button
            Rect invokeButtonRect = new Rect(objectFieldRect.xMax + spacing, objectFieldRect.y, 100, lineHeight); // Positioned to the right of object field

            // Get the serialized properties
            SerializedProperty channelProp = property.FindPropertyRelative("channel");
            SerializedProperty unityEventProp = property.FindPropertyRelative("OnEventRaised");

            // Draw the label with the event name
            string eventName = (channelProp.objectReferenceValue != null) ? channelProp.objectReferenceValue.name : "No Event Selected";
            EditorGUI.LabelField(labelRect, label, new GUIContent(eventName));

            // Show Unity Event fields
            EditorGUI.indentLevel++;
            float unityEventHeight = EditorGUI.GetPropertyHeight(unityEventProp, true);
            EditorGUI.PropertyField(
                new Rect(position.x, position.y + totalHeight, position.width, unityEventHeight),
                unityEventProp, true);
            EditorGUI.indentLevel--;

            // Draw the EventSO object field for dragging and dropping
            channelProp.objectReferenceValue = EditorGUI.ObjectField(
                objectFieldRect,
                channelProp.objectReferenceValue,
                typeof(EventSO),
                false // Set allowSceneObjects to false to prevent dragging scene objects
            );

            // Draw the "Debug Invoke" button
            if (GUI.Button(invokeButtonRect, "Debug Invoke"))
            {
                EventSO channel = channelProp.objectReferenceValue as EventSO;
                if (channel != null)
                {
                    // Invoke the event with the sampleArgs
                    channel.Raise(42, "Hello", Vector3.one);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Calculate the height for each row (label, object field, and button) and additional space for the Unity Event
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float totalHeight = (lineHeight + spacing) * 3; // Three rows for label, object field, and button

            SerializedProperty unityEventProp = property.FindPropertyRelative("OnEventRaised");
            totalHeight += EditorGUI.GetPropertyHeight(unityEventProp, true);

            return totalHeight;
        }
    }
}
