# LINQExtensions

A collection of extension methods for `IEnumerable`, `List` and arrays.

## Examples

# RandomElement/Shuffle/ToOneLineString/ForEach

```C#
int[] items = new int[] {1, 2, 3, 4, 5};

// Gets a random item
Debug.Log(items.RandomElement());

// Shuffles the array in place
items.Shuffle();

// Outputs the array on one line. Great for debugging.
// Example output:
// "3", "5", "2", "1", "4"
Debug.Log(items.ToOneLineString());

// Applies the function that takes the respective type to all the elements
items.ForEach(i => Debug.Log(i));
```

# Nearest

```C#
public class LINQExtensionsExamples : MonoBehaviour
{
	[SerializeField] Transform[] elements;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// Get the x/y position of the click (for an orthographic camera)
			var mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			// Gets the element closest to the mouse click
			var nearestElement = elements.Nearest(mousePositionWorld);
			
			Debug.Log("Nearest element " + nearestElement.name + " at position " + nearestElement.position);
		}
	}
}
```

## Dependencies

None.
