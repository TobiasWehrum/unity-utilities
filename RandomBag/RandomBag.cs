using System;
using System.Collections.Generic;

namespace UnityUtilities
{
    /// <summary>
    /// A RandomBag ensures that per interval [fillings * itemCount], every different item in it
    /// will be given back [fillings] times. Once the bag is empty, it is automatically refilled,
    /// either from a fixed array or by calling a delegate.
    /// 
    /// An example for that is used in some implementations of Tetris. The bag is filled with one
    /// instance (fillings=1) of each of the seven different pieces. Every time the next piece is
    /// needed, a random one is taken out of the bag until the bag is empty. That way, any two pieces
    /// are never longer than 7 pulls apart - and even that is only the case if the first and the last
    /// piece in the bag are the same one.
    /// </summary>
    /// <typeparam name="T">The type to be put in the bag.</typeparam>
    public class RandomBag<T>
    {
        /// <summary>
        /// A temporary array used internally for shuffling.
        /// </summary>
        List<T> tempList;

        /// <summary>
        /// The bag itself.
        /// </summary>
        Stack<T> bag;

        /// <summary>
        /// One of the two methods of filling the bag. When empty, this array is added to the bag [fillings] times.
        /// </summary>
        T[] refillItems;

        /// <summary>
        /// One of the two methods of filling the bag. When empty, this delegate is called [fillings] times to fill the bag.
        /// </summary>
        Action<List<T>> refillDelegate;

        /// <summary>
        /// How often <see cref="refillItems"/>/<see cref="refillDelegate"/> is used to fill the bag when the bag is empty.
        /// </summary>
        public int Fillings { get; private set; }

        /// <summary>
        /// Initializes the bag with a array of items to refill it and the number of times the array is added to the bag.
        /// Once the bag is empty, the array will automatically be added to the bag [fillings] times again.
        /// </summary>
        /// <param name="refillItems">The array to fill the bag [fillings] times with.</param>
        /// <param name="fillings">The number of times the array is added to the bag.</param>
        public RandomBag(T[] refillItems, int fillings)
        {
            bag = new Stack<T>();
            tempList = new List<T>();

            SetRefillItems(refillItems);
            Fillings = fillings;

            Reset();
        }

        /// <summary>
        /// Initializes the bag by calling the refillDelegate multiple times.
        /// Once the bag is empty, the delegate will automatically be called [fillings] times again.
        /// </summary>
        /// <param name="refillDelegate">The delegate that will be called [fillings] times to fill the bag.</param>
        /// <param name="fillings">The number of times refillDelegate is called.</param>
        public RandomBag(Action<List<T>> refillDelegate, int fillings)
        {
            bag = new Stack<T>();
            tempList = new List<T>();

            SetRefillDelegate(refillDelegate);
            Fillings = fillings;

            Reset();
        }

        /// <summary>
        /// Sets the refill method to "When empty, this delegate is called [fillings] times to fill the bag."
        /// </summary>
        /// <param name="refillDelegate">The delegate that will be called [fillings] times to fill the bag once it's empty.</param>
        public void SetRefillDelegate(Action<List<T>> refillDelegate)
        {
            this.refillDelegate = refillDelegate;
            refillItems = null;
        }

        /// <summary>
        /// Sets the refill method to "When empty, this array is added to the bag [fillings] times."
        /// </summary>
        /// <param name="refillItems">The array to fill the bag [fillings] times with once it's empty.</param>
        public void SetRefillItems(T[] refillItems)
        {
            if (refillItems.Length == 0)
                throw new ArgumentOutOfRangeException("refillItems", "RefillItems needs to contain at least one item.");

            this.refillItems = refillItems;
            refillDelegate = null;
        }

        /// <summary>
        /// Clears the bag and refills it.
        /// </summary>
        public void Reset()
        {
            // Empty the bag
            bag.Clear();

            // Call the delegate/add the array [Fillings] times
            for (var i = 0; i < Fillings; i++)
            {
                if (refillDelegate != null)
                {
                    refillDelegate(tempList);

                    if (tempList.Count == 0)
                        throw new Exception("RandomBag.refillDelegate didn't add any items to the bag.");
                }
                else
                {
                    tempList.AddRange(refillItems);
                }
            }

            // Shuffle the temporary list
            tempList.Shuffle();

            // Fill the temporary list into the bag 
            for (var i = 0; i < tempList.Count; i++)
            {
                bag.Push(tempList[i]);
            }

            // Clear the temporary list
            tempList.Clear();
        }

        /// <summary>
        /// Gets a random item from the bag. If there is no item in the bag, it is automatically refilled first.
        /// </summary>
        /// <returns>A random item from the bag.</returns>
        public T PopRandomItem()
        {
            // If the bag is empty, refill it
            if (bag.Count == 0)
                Reset();

            // Return an item
            return bag.Pop();
        }

        /// <summary>
        /// Gets multiple random items from the bag. If there aren't enough item in the bag, it is automatically refilled.
        /// </summary>
        /// <param name="count">How many items to pull from the bag.</param>
        /// <returns>A number of random items from the bag.</returns>
        public T[] PopRandomItems(int count)
        {
            var randomItems = new T[count];

            // Add [count] items to the bag
            for (var i = 0; i < count; i++)
            {
                // If the bag is empty, refill it
                if (bag.Count == 0)
                    Reset();

                // Add a random item to the result array
                randomItems[i] = bag.Pop();
            }

            return randomItems;
        }
        
        /// <summary>
        /// Gets an enumerator returning an endless number of items, automatically refilling the bag when empty.
        /// </summary>
        /// <returns>An enumerator returning an endless number of items from the bag.</returns>
        public IEnumerator<T> GetEndlessEnumerator()
        {
            while (true)
                yield return PopRandomItem();
        }

        /// <summary>
        /// Gets an enumerator returning the items remaining in the bag.
        /// </summary>
        /// <returns>An enumerator returning the items currently remaining in the bag.</returns>
        public IEnumerator<T> GetRemainderEnumerator()
        {
            while (bag.Count > 0)
                yield return bag.Pop();
        }

        /// <summary>
        /// The items currently remaining in the bag.
        /// </summary>
        public int RemainderCount
        {
            get { return bag.Count; }
        }
    }
}
