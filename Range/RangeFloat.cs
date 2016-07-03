using System;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// Provides input for a float value range in the editor and various convencience functions to work with that range.
    /// </summary>
    [Serializable]
    public class RangeFloat
    {
        /// <summary>
        /// The range start.
        /// </summary>
        [SerializeField] float from;

        /// <summary>
        /// The range end.
        /// </summary>
        [SerializeField] float to;

        /// <summary>
        /// The range start.
        /// </summary>
        public float From { get { return from; } }

        /// <summary>
        /// The range end.
        /// </summary>
        public float To { get { return to; } }

        /// <summary>
        /// Returns how wide the range between from and to is.
        /// </summary>
        public float Range { get { return to - from; } }

        /// <summary>
        /// Returns a random number between from [inclusive] and to [inclusive].
        /// </summary>
        public float RandomInclusive { get { return UnityEngine.Random.Range(from, to); } }

        /// <summary>
        /// Create a RangeFloat with 0-0 as the range. Needed for the editor.
        /// </summary>
        public RangeFloat()
        {
        }

        /// <summary>
        /// Create a RangeFloat with a certain range.
        /// </summary>
        /// <param name="from">Lower range bound.</param>
        /// <param name="to">Upper range bound.</param>
        public RangeFloat(float @from, float to)
        {
            this.@from = @from;
            this.to = to;
        }

        /// <summary>
        /// Linearly interpolates between to and from by t.
        /// </summary>
        /// <param name="t">How much to interpolate. Clamped between 0 and 1. 0 is [to] and 1 is [from].</param>
        /// <returns></returns>
        public float Lerp(float t)
        {
            return Mathf.Lerp(from, to, t);
        }

        /// <summary>
        /// Linearly interpolates between to and from by t.
        /// </summary>
        /// <param name="t">How much to interpolate. 0 is [to] and 1 is [from].</param>
        /// <returns></returns>
        public float LerpUnclamped(float t)
        {
            return Mathf.LerpUnclamped(from, to, t);
        }

        /// <summary>
        /// Calculates the linear parameter t that produces the interpolant value within the range [from, to].
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float InverseLerp(float value)
        {
            return Mathf.InverseLerp(from, to, value);
        }
    }
}