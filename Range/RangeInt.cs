using System;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// Provides input for a value range in the editor and various convencience functions to work with that range.
    /// </summary>
    [Serializable]
    public class RangeInt
    {
        /// <summary>
        /// The range start.
        /// </summary>
        [SerializeField] private int from;

        /// <summary>
        /// The range end.
        /// </summary>
        [SerializeField] private int to;

        /// <summary>
        /// The range start.
        /// </summary>
        public int From { get { return from; } }

        /// <summary>
        /// The range end.
        /// </summary>
        public int To { get { return to; } }

        /// <summary>
        /// Returns how wide the range between from and to is.
        /// </summary>
        public int Range { get { return to - from; } }

        /// <summary>
        /// Returns a random number between from [inclusive] and to [inclusive].
        /// </summary>
        public int RandomInclusive { get { return UnityEngine.Random.Range(from, to + 1); } }

        /// <summary>
        /// Returns a random number between from [inclusive] and to [exclusive].
        /// </summary>
        public int RandomExclusive { get { return UnityEngine.Random.Range(from, to); } }

        public RangeInt()
        {
        }

        public RangeInt(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
    }
}