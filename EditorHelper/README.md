# EditorHelper

Contains a method for getting the `[Tooltip]` attribute content of fields for editor classes. I might add more helper methods in the future.
 
## Example

```C#
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

		// Draw the GUI
		// [...]

		EditorGUI.EndProperty();
	}
}
```

## Dependencies

None.