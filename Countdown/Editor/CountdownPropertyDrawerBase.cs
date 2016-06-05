using UnityEditor;
using UnityEngine;

namespace UnityUtilities
{
    public class CountdownPropertyDrawerBase : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Set tooltip, if any
            label.tooltip = EditorHelper.GetTooltip(fieldInfo);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child field be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Draw the time field - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(position, property.FindPropertyRelative("time"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}