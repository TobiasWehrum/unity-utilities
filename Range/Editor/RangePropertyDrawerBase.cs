using UnityEditor;
using UnityEngine;

namespace UnityUtilities
{
    public class RangePropertyDrawerBase : PropertyDrawer
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

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var fromLabelWidth = 40;
            var toLabelWidth = 20;
            var afterValueFieldOffset = 5;
            var valuesWidth = (position.width - fromLabelWidth - toLabelWidth - afterValueFieldOffset) / 2f;
            var fromLabelRect = new Rect(position.x, position.y, fromLabelWidth, position.height);
            var fromRect = new Rect(fromLabelRect.xMax, position.y, valuesWidth, position.height);
            var toLabelRect = new Rect(fromRect.xMax + afterValueFieldOffset, position.y, toLabelWidth, position.height);
            var toRect = new Rect(toLabelRect.xMax, position.y, valuesWidth, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.LabelField(fromLabelRect, "From");
            EditorGUI.PropertyField(fromRect, property.FindPropertyRelative("from"), GUIContent.none);
            EditorGUI.LabelField(toLabelRect, "to");
            EditorGUI.PropertyField(toRect, property.FindPropertyRelative("to"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}