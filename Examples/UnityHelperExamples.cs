using UnityEngine;

#pragma warning disable 219 // We don't care about: warning CS0219: The variable `[...]' is assigned but its value is never used
namespace UnityUtilities.Examples
{
    public class UnityHelperExamples : MonoBehaviour
    {
        [SerializeField] Transform someEnemyTransform;
        [SerializeField] Transform enemyIndicator;
        [SerializeField] GameObject prefab;
        [SerializeField] LayerMask someLayerMask;
        [SerializeField] CharacterController playerCharacterController;

        void Awake()
        {
            TransformVectorColorExamples();
            CentroidExample();
            Vector2RotationExamples();
            GameObjectExamples();
            RectExamples();
            PlayerPrefsExample();
            CapsuleCastExample();
            RandomExamples();
            OtherExamples();

            // You can find an example for eased lerping in MathHelperExamples.EasedLerpFactorExample()
        }

        void TransformVectorColorExamples()
        {
            /* Set the transform.position.x to 5 and z to 3. Keeps y.
             * Equivalent to:
             * var position = transform.position;
             * position.x = 5;
             * position.z = 3;
             * transform.position = position;
             */
            transform.SetPosition(x: 5, z: 3);

            // Same as above; only sets transform.localEulerAngles.y.
            // There are extension methods for all position/rotation/scales.
            transform.SetLocalEulerAngles(y: 180);

            // Similar methods are available for Vector2/3/4 and Color:
            // Gets the transform.position, but with y set to 0.
            Vector3 floorPosition = transform.position.Change3(y: 0);

            // Gets the material color, but sets the color.a value to 0.5.
            Color halfTransparentColor = GetComponent<Renderer>().sharedMaterial.color.ChangeAlpha(0.5f);

            // Sets the position/rotation of enemyIndicator to someEnemyTransform.position/rotation
            enemyIndicator.CopyPositionAndRotatationFrom(someEnemyTransform);
        }

        private void CentroidExample()
        {
            Vector3[] list = {
                                new Vector3(-5, 10, 12),
                                new Vector3(55, 32, 10),
                                new Vector3(85, -40, 80)
                             };

            // Calculates the geometric center (the average) of the input list
            Debug.Log("Centroid: " + list.CalculateCentroid()); // => Centroid: (45.0, 0.7, 34.0)
        }

        void Vector2RotationExamples()
        {
            // Create a length 1 Vector2 pointing 40 degrees away from (1.0, 0.0)
            var vector = UnityHelper.CreateVector2AngleDeg(20f);
            Debug.Log(vector); // => (0.9, 0.3)

            // Rotate the vector 70 degrees
            vector = vector.RotateDeg(70f);
            Debug.Log(vector); // => (0.0, 1.0)

            // Output the current vector rotation
            Debug.Log(vector.GetAngleDeg()); // => 90        
        }

        void GameObjectExamples()
        {
            // Assigns layer 4 to this GameObject and all its children recursively
            gameObject.AssignLayerToHierarchy(4);

            // Create an instance of a prefab. When the prefab is named "Original", the instance will
            // be named "Original(Copy)"
            GameObject copiedGameObject = Instantiate(prefab);

            // Return the name without "(Copy)"
            Debug.Log(copiedGameObject.GetNameWithoutClone()); // => Original

            // Change the name back to "Original"
            copiedGameObject.StripCloneFromName();
        }

        void RectExamples()
        {
            // Make a rect from (10|20) to (60|120)
            Rect rect = new Rect(10, 20, 50, 100);

            // Gets a random position for an enemy in the rect, leaving a 5 unit border
            Vector2 enemySpawnPosition = rect.RandomPosition(-5);

            // Gets a random sub rect of size 10|10 in which we could spawn multiple enemies
            Rect enemySpawnSubrect = rect.RandomSubRect(10, 10);

            Vector2 enemyPosition = new Vector2(0, 500);

            // Clamp an enemy position to the rect
            enemyPosition = rect.Clamp2(enemyPosition);
            Debug.Log(enemyPosition); // Output: (10.0, 120.0)

            // Create a rect that is 10 units bigger to each side
            Rect biggerRect = rect.Extend(10);

            // Get the corner points
            Vector2[] cornerPoints = rect.GetCornerPoints();
        }

        void PlayerPrefsExample()
        {
            // Gets a PlayerPrefs key "FirstStart" or return true if not set
            bool isFirstStart = UnityHelper.PlayerPrefsGetBool("FirstStart", true);

            // Set the key FirstStart to false
            UnityHelper.PlayerPrefsSetBool("FirstStart", false);
        }

        void CapsuleCastExample()
        {
            Vector3 point1;
            Vector3 point2;
            float radius;
            Vector3 origin = playerCharacterController.transform.position;

            // Get the data for the capsule cast from the current player position
            UnityHelper.GetCapsuleCastData(playerCharacterController, origin, out point1, out point2, out radius);

            // Cast 2 units forwards
            bool hitSomething = Physics.CapsuleCast(point1, point2, radius, Vector3.forward, 2f);
        }

        void RandomExamples()
        {
            // Points in a random 2D direction
            var randomDirection2D = UnityHelper.RandomOnUnitCircle;

            // Either goes left or right
            var deltaX = 20 * UnityHelper.RandomSign;

            // Gets set to either choice
            var choice = UnityHelper.RandomBool ? "Choice A" : "Choice B";
        }

        void OtherExamples()
        {
            // Does the layer mask contain layer 4?
            bool containsLayer4 = someLayerMask.ContainsLayer(4);

            // Get the bounds of all colliders in the level to clamp the camera later on
            Collider[] allColliders = FindObjectsOfType<Collider>();
            Bounds levelBounds = UnityHelper.CombineColliderBounds(allColliders);

            // Find out how much the perspective camera can see at 10 unit away
            Vector2 viewportSizeAtDistance = Camera.main.CalculateViewportWorldSizeAtDistance(10);
        }
    }
}
#pragma warning restore 168