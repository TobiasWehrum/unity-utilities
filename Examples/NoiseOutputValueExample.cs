using UnityEngine;

namespace UnityUtilities.Examples
{
    public class NoiseOutputValueExample : MonoBehaviour
    {
        [SerializeField] NoiseOutputValue positionNoise;
        [SerializeField] Transform sphere;

        void Update()
        {
            // Updates the value with Time.deltaTime*speed
            positionNoise.Progress();

            // Sets the y position at the current output value
            sphere.transform.position = new Vector3(0, positionNoise.OutputValue, 0f);
        }
    }
}
