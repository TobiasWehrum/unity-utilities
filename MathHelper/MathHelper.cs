using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityUtilities
{
    /// <summary>
    /// The MathHelper contains methods to help with mapping and angles and a really nifty method for framerate-independent eased lerping.
    /// </summary>
    public static class MathHelper
    {
        #region Mapping

        /// <summary>
        /// Maps a value from [sourceFrom..sourceTo] to [targetFrom..targetTo] with clamping.
        /// 
        /// This is basically Mathf.Lerp(targetFrom, targetTo, Mathf.InverseLerp(sourceFrom, sourceTo, sourceValue)).
        /// </summary>
        /// <param name="sourceValue">The value in the range of [sourceFrom..sourceTo]. Will be clamped if not in that range.</param>
        /// <param name="sourceFrom">The lower end of the source range.</param>
        /// <param name="sourceTo">The higher end of the source range.</param>
        /// <param name="targetFrom">The lower end of the target range.</param>
        /// <param name="targetTo">The higher end of the target range.</param>
        /// <returns>The mapped value.</returns>
        public static float MapClamped(float sourceValue, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            var sourceRange = sourceTo - sourceFrom;
            var targetRange = targetTo - targetFrom;
            var percent = Mathf.Clamp01((sourceValue - sourceFrom) / sourceRange);
            return targetFrom + targetRange * percent;
        }

        /// <summary>
        /// Applies a deadzone [-deadzone..deadzone] in which the value will be set to 0.
        /// </summary>
        /// <param name="value">The joystick value.</param>
        /// <param name="deadzone">A value between for which all results [-deadzone..deadzone] will be set to 0.</param>
        /// <param name="fullRangeBetweenDeadzoneAndOne">If this is true, the values between [-1..-deadzone] and [deadzone..1] will be mapped to [-1..0] and [0..1] respectively.</param>
        /// <returns>The result value between [-1..1].</returns>
        public static float ApplyJoystickDeadzone(float value, float deadzone, bool fullRangeBetweenDeadzoneAndOne = false)
        {
            if (Mathf.Abs(value) <= deadzone)
                return 0;

            if (fullRangeBetweenDeadzoneAndOne && (deadzone > 0f))
            {
                if (value < 0)
                {
                    return MapClamped(value, -1f, -deadzone, -1f, 0f);
                }
                else
                {
                    return MapClamped(value, deadzone, 1f, 0f, 1f);
                }
            }

            return value;
        }

        /// <summary>
        /// Maps a joystick input from [sourceFrom..sourceTo] to [-1..1] with clamping.
        /// Applies a deadzone [-deadzone..deadzone] in which the value will be set to 0.
        /// </summary>
        /// <param name="sourceValue">The value in the range of [sourceFrom..sourceTo]. Will be clamped if not in that range.</param>
        /// <param name="sourceFrom">The lower end of the source range.</param>
        /// <param name="sourceTo">The higher end of the source range.</param>
        /// <param name="deadzone">A value between 0 and 1 for which all results [-deadzone..deadzone] will be set to 0.</param>
        /// <param name="fullRangeBetweenDeadzoneAndOne">If this is true, the values between [-1..-deadzone] and [deadzone..1] will be mapped to [-1..0] and [0..1] respectively.</param>
        /// <returns>The result value between [-1..1].</returns>
        public static float MapClampedJoystick(float sourceValue, float sourceFrom, float sourceTo, float deadzone = 0f, bool fullRangeBetweenDeadzoneAndOne = false)
        {
            var percent = MapClamped(sourceValue, sourceFrom, sourceTo, -1, 1);

            if (deadzone > 0)
                percent = ApplyJoystickDeadzone(percent, deadzone, fullRangeBetweenDeadzoneAndOne);

            return percent;
        }

        #endregion

        #region Angles

        /// <summary>
        /// Returns the closer center between two angles.
        /// </summary>
        /// <param name="angle1">The first angle.</param>
        /// <param name="angle2">The second angle.</param>
        /// <returns>The closer center.</returns>
        public static float GetCenterAngleDeg(float angle1, float angle2)
        {
            return angle1 + Mathf.DeltaAngle(angle1, angle2) / 2f;
        }

        /// <summary>
        /// Normalizes an angle between 0 (inclusive) and 360 (exclusive).
        /// </summary>
        /// <param name="angle">The input angle.</param>
        /// <returns>The result angle.</returns>
        public static float NormalizeAngleDeg360(float angle)
        {
            while (angle < 0)
                angle += 360;

            if (angle >= 360)
                angle %= 360;

            return angle;
        }

        /// <summary>
        /// Normalizes an angle between -180 (inclusive) and 180 (exclusive).
        /// </summary>
        /// <param name="angle">The input angle.</param>
        /// <returns>The result angle.</returns>
        public static float NormalizeAngleDeg180(float angle)
        {
            while (angle < -180)
                angle += 360;

            while (angle >= 180)
                angle -= 360;

            return angle;
        }

        #endregion

        #region Framerate-Independent Lerping

        /// <summary>
        /// Provides a framerate-independent t for lerping towards a target.
        /// 
        /// Example:
        /// 
        ///     currentValue = Mathf.Lerp(currentValue, 1f, MathHelper.EasedLerpFactor(0.75f);
        /// 
        /// will cover 75% of the remaining distance between currentValue and 1 each second.
        /// 
        /// There are essentially two ways of lerping a value over time: linear (constant speed) or
        /// eased (e.g. getting slower the closer you are to the target, see http://easings.net.)
        /// 
        /// For linear lerping (and most of the easing functions), you need to track the start and end
        /// positions and the time that elapsed.
        /// 
        /// Calling something like
        /// 
        ///     currentValue = Mathf.Lerp(currentValue, 1f, 0.95f);
        /// 
        /// every frame provides an easy way of eased lerping without tracking elapsed time or the
        /// starting value, but since it's called every frame, the actual traversed distance per
        /// second changes the higher the framerate is.
        /// 
        /// This function replaces the lerp T to make it framerate-independent and easier to estimate.
        /// 
        /// For more info, see https://www.scirra.com/blog/ashley/17/using-lerp-with-delta-time.
        /// </summary>
        /// <param name="factor">How much % the lerp should cover per second.</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The framerate-independent lerp t.</returns>
        public static float EasedLerpFactor(float factor, float deltaTime = 0f)
        {
            if (deltaTime == 0f)
                deltaTime = Time.deltaTime;

            return 1 - Mathf.Pow(1 - factor, deltaTime);
        }

        /// <summary>
        /// Framerate-independent eased lerping to a target value, slowing down the closer it is.
        /// 
        /// If you call
        /// 
        ///     currentValue = MathHelper.EasedLerp(currentValue, 1f, 0.75f);
        /// 
        /// each frame (e.g. in Update()), starting with a currentValue of 0, then after 1 second
        /// it will be approximately 0.75 - which is 75% of the way between 0 and 1.
        /// 
        /// Adjusting the target or the percentPerSecond between calls is also possible.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="percentPerSecond">How much of the distance between current and target should be covered per second?</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The interpolated value from current to target.</returns>
        public static float EasedLerp(float current, float target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = EasedLerpFactor(percentPerSecond, deltaTime);
            return Mathf.Lerp(current, target, t);
        }

        #endregion
    }
}