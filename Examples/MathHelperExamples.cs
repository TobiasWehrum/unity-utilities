
using UnityEngine;

#pragma warning disable 219 // We don't care about: warning CS0219: The variable `[...]' is assigned but its value is never used
namespace UnityUtilities.Examples
{
    public class MathHelperExamples : MonoBehaviour
    {
        [SerializeField] private Transform runner;
        [SerializeField] private Transform follower;

        void Awake()
        {
            MappingExamples();
            AnglesExamples();

            // Checks what calling the same EasedLerp from 0 to 1 and a factor of 75%
            // with 1, 10, 100, 1000 or 10000 FPS would return after one second.
            CheckEasedLerp(1);      // => 0.75
            CheckEasedLerp(10);     // => 0.7499999
            CheckEasedLerp(100);    // => 0.7499999
            CheckEasedLerp(1000);   // => 0.7499995
            CheckEasedLerp(10000);  // => 0.7500508
        }

        void Update()
        {
            EasedLerpFactorExample();
        }

        private void MappingExamples()
        {
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
        }

        private void AnglesExamples()
        {
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
        }

        private void EasedLerpFactorExample()
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

        void CheckEasedLerp(int steps)
        {
            var dt = 1f / steps;

            var currentValue = 0f;
            for (var i = 0; i < steps; i++)
                currentValue = MathHelper.EasedLerp(currentValue, 1f, 0.75f, dt);

            Debug.LogFormat("CheckEasedLerp({0}): {1}", steps, currentValue);
        }
    }
}
#pragma warning restore 168
                           
                           