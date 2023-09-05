using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityTools.ScriptableObjectFramework.Editor
{
    [CustomEditor(typeof(EventSO))]
    public class EventSOEditor : UnityEditor.Editor
    {
        private GUIStyle boldLabelStyle;
        private GUIStyle valueLabelStyle;

        public override void OnInspectorGUI()
        {
            EventSO eventSO = (EventSO)target;

            InitializeGUIStyles();

            EditorGUILayout.LabelField("Event Name: " + eventSO.name, boldLabelStyle);
            EditorGUILayout.Space();

            // Display all raised CustomArgs with timestamps
            List<EventSO.ArgsWithTimestamp> raisedArgs = eventSO.GetRaisedArgs();

            // Reverse the list to show the latest CustomArgs at the top
            raisedArgs.Reverse();

            if (raisedArgs.Count > 0)
            {
                EditorGUILayout.LabelField("Raised Args with Timestamps:", boldLabelStyle);
                EditorGUI.indentLevel++;

                foreach (EventSO.ArgsWithTimestamp argsWithTimestamp in raisedArgs)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Timestamp: " + argsWithTimestamp.Timestamp.ToString(), boldLabelStyle);
                    DisplayCustomArgs(argsWithTimestamp.Args);
                }

                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUILayout.LabelField("No Raised Args", boldLabelStyle);
            }
        }

        private void DisplayCustomArgs(CustomArgs i_args)
        {
            EditorGUILayout.LabelField("Parameters Count: " + i_args.GetParamsCount(), boldLabelStyle);

            // Begin the table (Header row)
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Index", boldLabelStyle, GUILayout.Width(50));
            EditorGUILayout.LabelField("Parameter Type", boldLabelStyle);
            EditorGUILayout.LabelField("Parameter Value", boldLabelStyle);
            EditorGUILayout.EndHorizontal();

            // Loop through each parameter and display in the table
            for (int i = 0; i < i_args.GetParamsCount(); i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50));
                EditorGUILayout.LabelField(i_args.GetParams()[i].GetType().ToString(), valueLabelStyle);
                EditorGUILayout.LabelField(i_args.GetParams()[i].ToString(), valueLabelStyle);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void InitializeGUIStyles()
        {
            if (boldLabelStyle == null)
            {
                boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
            }

            if (valueLabelStyle == null)
            {
                valueLabelStyle = new GUIStyle(EditorStyles.label);
                valueLabelStyle.fontStyle = FontStyle.Italic;
            }
        }
    }
}
