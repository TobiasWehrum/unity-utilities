# EditorHelper

The `EditorHelper` contains static helper methods for editor classes. Right now, there's only one of them for getting the tooltip of a field.
 
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