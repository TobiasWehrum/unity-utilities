# RandomBag

A RandomBag ensures that per interval [fillings * itemCount], every different item in it will be given back [fillings] times. Once the bag is empty, it is automatically refilled, either from a fixed array or by calling a delegate.

An example for that is used in [some implementations of Tetris](http://tetris.wikia.com/wiki/Random_Generator). The bag is filled with one instance (fillings=1) of each of the seven different pieces. Every time the next piece is needed, a random one is taken out of the bag until the bag is empty. That way, any two pieces are never longer than 14 pulls apart - and even that is only the case if the first and the last piece in the bag are the same one.

## Example

```C#
public class RandomBagExample : MonoBehaviour
{
	private enum TetrisPiece
	{
		Line, Square, T, J, L, S, Z
	}

	RandomBag<TetrisPiece> pieceBag;

	void Awake()
	{
		// Get an array with each value of TetrisPiece
		TetrisPiece[] tetrisPieceArray = (TetrisPiece[]) Enum.GetValues(typeof (TetrisPiece));

		// Create the bag containing two instances of every value of TetrisPiece
		pieceBag = new RandomBag<TetrisPiece>(tetrisPieceArray, 2);

		// Gets 50 items from the bag. The bag will be filled with 14 TetrisPieces and
		// automatically refilled with 14 more when needed. No two pieces will ever be
		// more than 14 calls apart - and even that will only happen if that piece was
		// the first and last item in the current 14 piece bag filling.
		StringBuilder str = new StringBuilder();
		for (var i = 0; i < 50; i++)
		{
			str.Append(pieceBag.PopRandomItem());
			str.Append(", ");
		}

		Debug.Log(str);

		// Example output:
		// T, Z, J, Square, Z, S, L, L, S, Line, J, Line, Square, T, Square, Z,
		// S, T, Line, Square, Z, T, J, Line, S, L, J, L, S, Z, Line, J, Line,
		// J, L, L, S, T, T, Z, Square, Square, Z, T, S, Z, J, J, L, Line, 
	}
}
```

## Dependencies

* [LINQExtensions](https://github.com/TobiasWehrum/unity-utilities/tree/master/LINQExtensions)