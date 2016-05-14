using System;

namespace UnityUtilities
{
    /// <summary>
    /// A version of <see cref="Countdown"/> that can be set in the Editor.
    /// Refreshes the total time on each loop to allow for live editing. Loops.
    /// </summary>
    [Serializable]
    public class CountdownPropertyLoop : CountdownProperty
    {
        /// <summary>
        /// Empty constructor for the editor.
        /// </summary>
        public CountdownPropertyLoop() : base(true)
        {
        }
    }
}