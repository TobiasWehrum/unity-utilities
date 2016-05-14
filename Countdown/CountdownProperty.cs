using System;
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

        /// <summary>
        /// Called before the serialization (reading the editor value) of the "time" property happens.
        /// It's empty and just here because <see cref="ISerializationCallbackReceiver"/> needs it.
        /// </summary>
        public void OnBeforeSerialize()
        {
        }

        /// <summary>
        /// Called after the serialization (reading the editor value) of the "time" property happens.
        /// If the timer is not initialized yet, it will be here.
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (initialized)
                return;

            initialized = true;
            Reset();
        }
    }
}