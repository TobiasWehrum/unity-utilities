# MathHelper

The MathHelper contains methods to help with mapping and angles and a really nifty method for framerate-independent eased lerping.

## Examples

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

### Mapping

```C#
// Maps 10 from [-250..250] to [0..10]
Debug.Log(MathHelper.MapClamped(10f, -250f, 250f, 0f, 10f));    // => 5.2

// Applies a deadzone to a joystick input (positive and negative) to make sure that
// little imperfections in the stick resting position don't make the character move
Debug.Log(MathHelper.ApplyJoystickDeadzone(0.1f, 0.2f));        // => 0
Debug.Log(MathHelper.ApplyJoystickDeadzone(0.2f, 0.2f));        // => 0
Debug.Log(MathHelper.ApplyJoystickDeadzone(0.21f, 0.2f));       // => 0.21
Debug.Log(MathHelper.ApplyJoystickDeadzone(0.3f, 0.2f));        // => 0.3
Debug.Log(MathHelper.ApplyJoystickDeadzone(1f, 0.2f));          // => 1
Debug.Log(MathHelper.ApplyJoystickDeadzone(-0.1f, 0.2f));       // => 0
Debug.Log(MathHelper.ApplyJoystickDeadzone(-0.2f, 0.2f));       // => 0
Debug.Log(MathHelper.ApplyJoystickDeadzone(-0.21f, 0.2f));      // => -0.21
Debug.Log(MathHelper.ApplyJoystickDeadzone(-0.3f, 0.2f));       // => -0.3
Debug.Log(MathHelper.ApplyJoystickDeadzone(-1f, 0.2f));         // => -1
```

### Angles

```C#
// Gets the center angle between two angles
Debug.Log(MathHelper.GetCenterAngleDeg(20f, 160f));     // => 90
Debug.Log(MathHelper.GetCenterAngleDeg(20f, 220f));     // => -60
Debug.Log(MathHelper.GetCenterAngleDeg(20f, -140f));    // => -60

// Normalizes an angle between 0 (inclusive) and 360 (exclusive).
Debug.Log(MathHelper.NormalizeAngleDeg360(-180f));      // => 180
Debug.Log(MathHelper.NormalizeAngleDeg360(180f));       // => 180
Debug.Log(MathHelper.NormalizeAngleDeg360(0f));         // => 0
Debug.Log(MathHelper.NormalizeAngleDeg360(360f));       // => 0
Debug.Log(MathHelper.NormalizeAngleDeg360(340f));       // => 340

// Normalizes an angle between -180 (inclusive) and 180 (exclusive).
Debug.Log(MathHelper.NormalizeAngleDeg180(-180f));      // => -180
Debug.Log(MathHelper.NormalizeAngleDeg180(180f));       // => -180
Debug.Log(MathHelper.NormalizeAngleDeg180(0f));         // => 0
Debug.Log(MathHelper.NormalizeAngleDeg180(360f));       // => 0
Debug.Log(MathHelper.NormalizeAngleDeg180(340f));       // => -20
```

## Dependencies

None.