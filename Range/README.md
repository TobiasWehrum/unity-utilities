# RangeInt/Float

`RangeInt`/`RangeFloat` are editable data types that take an int/float range. Used for things like "Spawn 2 to 4 enemies."

## Example
![RangeExample Editor Screenshot](https://raw.githubusercontent.com/TobiasWehrum/unity-utilities/master/_Images/RangeExample.png)

```C#
public class RangeExample : MonoBehaviour
    {
        [SerializeField] RangeInt amountRange;
        [SerializeField] RangeFloat numberRange;

        private void Awake()
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
```

## Dependencies

None.