# RollingArray

`RollingArray` is a container that always stores the last arraySize elements added. New elements are added via `Append()`, which automatically rolls over once the maximum number of elements is reached, overwriting the oldest element. `array[i]` always returns the oldest element that still exists + i. That way, this container always stores the last arraySize elements added.
 
Adding is O(1), retrieving is O(1) and (with the exception of `GetEnumerator()`) no new memory is allocated after the initial creation.

You can read more about the `GetEnumerator()` memory problem (and Unity's dreadful memory allocation troubles) here under "Should you avoid foreach loops?": http://www.gamasutra.com/blogs/WendelinReich/20131109/203841/C_Memory_Management_for_Unity_Developers_part_1_of_3.php

## Example

```C#
public class RollingArrayExample : MonoBehaviour
{
	[SerializeField] Transform indicatorObject;

	RollingArray<Vector2> mousePositions;
	Camera mainCamera;

	void Awake()
	{
		// Save the last 50 elements
		mousePositions = new RollingArray<Vector2>(50);

		// Cache a reference to the main camera
		mainCamera = Camera.main;
	}

	void FixedUpdate()
	{
		// Get the mouse position in a fixed interval
		// If we get to 50 positions, the oldest position will be replaced
		mousePositions.Append(mainCamera.ScreenToWorldPoint(Input.mousePosition));
	}

	void Update()
	{
		// Only continue if we have at least one mouse position
		if (mousePositions.IsEmpty)
			return;

		// Go through all the saved mouse positions from oldest to newest to get the average
		Vector2 averagePosition = new Vector2();
		for (var i = 0; i < mousePositions.Count; i++)
		{
			averagePosition += mousePositions[i];
		}
		averagePosition /= mousePositions.Count;

		// Set the indicator object to the average position
		indicatorObject.position = averagePosition;
	}
}
```

## Dependencies

None.