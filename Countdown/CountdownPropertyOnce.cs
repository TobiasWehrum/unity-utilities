using System;
using System.Collections;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A version of <see cref="Countdown"/> that can be directly filled in the Editor.
    /// Just called once, but refreshes the total time on each call to <see cref="Countdown.Reset()"/>.
    /// </summary>
    [Serializable]
    public class CountdownPropertyOnce : CountdownProperty
    {
        /// <summary>
        /// Empty constructor for the editor.
        /// </summary>
        public CountdownPropertyOnce() : base(false)
        {
        }
    }
}