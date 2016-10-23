using UnityEngine;

namespace UnityUtilities.Examples
{
    public class RollingArrayExample : MonoBehaviour
    {
        [SerializeField] Transform indicatorObject;

        RollingArray<Vector2> mousePositions;
        Camera mainCamera;

        void Awake()
        {
            // Save the last 50 elements
            mousePositions = new RollingArray<Vector2>(50);

            // Cache a reference to the main camera
            mainCamera = Camera.main;
        }

        void FixedUpdate()
        {
            // Get the mouse position in a fixed interval
            // If we get to 50 positions, the oldest position will be replaced
            mousePositions.Append(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }

        void Update()
        {
            // Only continue if we have at least one mouse position
            if (mousePositions.IsEmpty)
                return;

            // Go through all the saved mouse positions from oldest to newest to get the average
            Vector2 averagePosition = new Vector2();
            for (var i = 0; i < mousePositions.Count; i++)
            {
                averagePosition += mousePositions[i];
            }
            averagePosition /= mousePositions.Count;

            // Set the indicator object to the average position
            indicatorObject.position = averagePosition;
        }
    }
}