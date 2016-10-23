# UnityHelper

The UnityHelper contains a plethora of useful extensions and helpers for Transform, GameObject, Vector2/3/4, Rect and more.

## Examples

### Transform/Vector/Color

```C#
/* Set the transform.position.x to 5 and z to 3. Keeps y.
 * Equivalent to:
 * var position = transform.position;
 * position.x = 5;
 * position.z = 3;
 * transform.position = position;
 */
transform.SetPosition(x: 5, z: 3);

// Same as above; only sets transform.localEulerAngles.y.
// There are extension methods for all position/rotation/scales.
transform.SetLocalEulerAngles(y: 180);

// Similar methods are available for Vector2/3/4 and Color:
// Gets the transform.position, but with y set to 0.
Vector3 floorPosition = transform.position.Change3(y: 0);

// Gets the material color, but sets the color.a value to 0.5.
Color halfTransparentColor = GetComponent<Renderer>().sharedMaterial.color.ChangeAlpha(0.5f);

// Sets the position/rotation of enemyIndicator to someEnemyTransform.position/rotation
enemyIndicator.CopyPositionAndRotatationFrom(someEnemyTransform);
```

### Framerate-Independent Eased Lerping

There are essentially two ways of lerping a value over time: linear (constant speed) or eased (e.g. getting slower the closer you are to the target, see http://easings.net.)

For linear lerping (and most of the easing functions), you need to track the start and end positions and the time that elapsed.

Calling something like `currentValue = Mathf.Lerp(currentValue, 1f, 0.95f);` every frame provides an easy way of eased lerping without tracking elapsed time or the starting value, but since it's called every frame, the actual traversed distance per second changes the higher the framerate is.

EasedLerpFactor replaces the lerp parameter t to make it framerate-independent and easier to estimate.

You can find more information about the formula used [here](https://www.scirra.com/blog/ashley/17/using-lerp-with-delta-time).

You can use `MathHelper.EasedLerpFactor` to get the t used for a lerp or call `MathHelper.EasedLerp`, `UnityHelper.EasedLerpVector2`, `UnityHelper.EasedLerpVector3`, `UnityHelper.EasedLerpVector4` or `UnityHelper.EasedLerpColor` directly.

![EasedLerpFactorExample Editor Screenshot](https://raw.githubusercontent.com/TobiasWehrum/unity-utilities/master/_Images/EasedLerpFactorExample.gif)

```C#
void Update()
{
	// Get the world position of the mouse pointer
	Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	mousePositionWorld.z = 0f;

	// Set the runner position to the mouse pointer
	runner.position = mousePositionWorld;

	// Move the follower 75% of the remaining distance to the runner per second
	follower.position = UnityHelper.EasedLerpVector3(follower.position, runner.position, 0.75f);

	// ...which is the same as:

	//float t = MathHelper.EasedLerpFactor(0.75f);
	//follower.position = Vector3.Lerp(follower.position, mousePositionWorld, t);
}
```

### Get the centroid from an array/list of Vector2/3/4

```C#
Vector3[] list = {
					new Vector3(-5, 10, 12),
					new Vector3(55, 32, 10),
					new Vector3(85, -40, 80)
				 };

// Calculates the geometric center (the average) of the input list
Debug.Log("Centroid: " + list.CalculateCentroid()); // => Centroid: (45.0, 0.7, 34.0)
```

### Vector2 Rotation

```C#
// Create a length 1 Vector2 pointing 40 degrees away from (1.0, 0.0)
var vector = UnityHelper.CreateVector2AngleDeg(20f);
Debug.Log(vector); // => (0.9, 0.3)

// Rotate the vector 70 degrees
vector = vector.RotateDeg(70f);
Debug.Log(vector); // => (0.0, 1.0)

// Output the current vector rotation
Debug.Log(vector.GetAngleDeg()); // => 90
```

### GameObject

```C#
// Assigns layer 4 to this GameObject and all its children recursively
gameObject.AssignLayerToHierarchy(4);

// Create an instance of a prefab. When the prefab is named "Original", the instance will
// be named "Original(Copy)"
GameObject copiedGameObject = Instantiate(prefab);

// Return the name without "(Copy)"
Debug.Log(copiedGameObject.GetNameWithoutClone()); // => Original

// Change the name back to "Original"
copiedGameObject.StripCloneFromName();
```

### Rect

```C#
// Make a rect from (10|20) to (60|120)
Rect rect = new Rect(10, 20, 50, 100);

// Gets a random position for an enemy in the rect, leaving a 5 unit border
Vector2 enemySpawnPosition = rect.RandomPosition(-5);

// Gets a random sub rect of size 10|10 in which we could spawn multiple enemies
Rect enemySpawnSubrect = rect.RandomSubRect(10, 10);

Vector2 enemyPosition = new Vector2(0, 500);

// Clamp an enemy position to the rect
enemyPosition = rect.Clamp2(enemyPosition);
Debug.Log(enemyPosition); // Output: (10.0, 120.0)

// Create a rect that is 10 units bigger to each side
Rect biggerRect = rect.Extend(10);

// Get the corner points
Vector2[] cornerPoints = rect.GetCornerPoints();
```

### Random

```C#
// Points in a random 2D direction
var randomDirection2D = UnityHelper.RandomOnUnitCircle;

// Either goes left or right
var deltaX = 20 * UnityHelper.RandomSign;

// Gets set to either choice
var choice = UnityHelper.RandomBool ? "Choice A" : "Choice B";
```

### PlayerPrefs: Bool

```C#
// Gets a PlayerPrefs key "FirstStart" or return true if not set
bool isFirstStart = UnityHelper.PlayerPrefsGetBool("FirstStart", true);

// Set the key FirstStart to false
UnityHelper.PlayerPrefsSetBool("FirstStart", false);
```

### Check if a layer is included in a LayerMask

```C#
// Does the layer mask contain layer 4?
bool containsLayer4 = someLayerMask.ContainsLayer(4);
```

### Find out how big the level bounds are

```C#
// Get the bounds of all colliders in the level to clamp the camera later on
Collider[] allColliders = FindObjectsOfType<Collider>();
Bounds levelBounds = UnityHelper.CombineColliderBounds(allColliders);
```

### Calculate Camera viewport world size at distance

```C#
// Find out how much the perspective camera can see at 10 unit away
Vector2 viewportSizeAtDistance = Camera.main.CalculateViewportWorldSizeAtDistance(10);
```

### CharacterController: CapsuleCast

```C#
Vector3 point1;
Vector3 point2;
float radius;
Vector3 origin = playerCharacterController.transform.position;

// Get the data for the capsule cast from the current player position
UnityHelper.GetCapsuleCastData(playerCharacterController, origin, out point1, out point2, out radius);

// Cast 2 units forwards
bool hitSomething = Physics.CapsuleCast(point1, point2, radius, Vector3.forward, 2f);
```

## Dependencies

* [MathHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/MathHelper)