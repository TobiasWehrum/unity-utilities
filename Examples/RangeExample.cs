using UnityEngine;

namespace UnityUtilities.Examples
{
    public class RangeExample : MonoBehaviour
    {
        [SerializeField] RangeInt amountRange;
        [SerializeField] RangeFloat numberRange;

        void Awake()
        {
            // Get a random number in amountRange
            int amount = amountRange.RandomInclusive;

            // Output [amount] numbers
            for (int i = 0; i < amount; i++)
            {
                // Transform [i..(amount-1)] to [0..1]
                var t = ((float)i / (amount - 1));

                // Mathf.Lerp(numberRange.From, numberRange.To, t)
                Debug.Log(numberRange.Lerp(t));
            }
        }
    }
}
