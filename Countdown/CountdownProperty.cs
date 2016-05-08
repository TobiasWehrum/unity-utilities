using System;
using System.Collections;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A base version for properties based on <see cref="Countdown"/>.
    /// </summary>
    [Serializable]
    public class CountdownProperty : Countdown, ISerializationCallbackReceiver
    {
        /// <summary>
        /// The total time to be set. Refreshed at each loop to allow for live editing.
        /// </summary>
        [SerializeField] float time;

        bool initialized;

        /// <summary>
        /// Empty constructor for the editor.
        /// </summary>
        public CountdownProperty(bool loop) : base(loop)
        {
            CalculateDurationDelegate = GetTimeProperty;
        }

        /// <summary>
        /// The method used to set the total time each loop.
        /// </summary>
        /// <returns>The value of the <see cref="time"/> field.</returns>
        float GetTimeProperty()
        {
            return time;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (initialized)
                return;

            initialized = true;
            Reset();
        }
    }
}