using UnityEngine;

namespace UnityUtilities.Examples
{
    public class CountdownPropertyExample : MonoBehaviour
    {
        [Tooltip("Test")]
        [SerializeField] CountdownPropertyOnce countdownOnce;
        [SerializeField] CountdownPropertyLoop countdownLoop;

        void Update()
        {
            // Looping automatically
            if (countdownLoop.Progress())
                Debug.Log("This is logged every " + countdownLoop.Duration + " seconds.");

            // Not looping
            if (countdownOnce.Progress())
                Debug.Log("This is shown once after " + countdownOnce.Duration + " seconds.");

            // Start countdownOnce again - if it's already running, it is reset
            if (Input.GetKeyDown(KeyCode.Space))
                countdownOnce.Reset();
        }
    }
}