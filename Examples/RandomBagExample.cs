using System;
using System.Text;
using UnityEngine;

namespace UnityUtilities.Examples
{
    public class RandomBagExample : MonoBehaviour
    {
        enum TetrisPiece
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
}
