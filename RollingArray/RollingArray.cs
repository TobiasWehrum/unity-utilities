using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A container that always stores the last arraySize elements added. New elements are added
    /// via Append(), which automatically rolls over once the maximum number of elements is reached,
    /// overwriting the oldest element. array[i] always returns the oldest element that still
    /// exists + i. That way, this container always stores the last arraySize elements added.
    /// 
    /// Adding is O(1), retrieving is O(1) and (with the exception of GetEnumerator()) no new memory
    /// is allocated after the initial creation.
    /// </summary>
    /// <typeparam name="T">The collection element type.</typeparam>
    public class RollingArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// The internal storage array.
        /// </summary>
        T[] array;

        /// <summary>
        /// The index of the next element to be added.
        /// </summary>
        int nextElementIndex;

        /// <summary>
        /// The current count of filled elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Creates a new rolling array with the defined arraySize.
        /// </summary>
        /// <param name="arraySize"></param>
        public RollingArray(int arraySize)
        {
            array = new T[arraySize];
        }

        /// <summary>
        /// Appends an element to the RollingArray. Automatically rolls over once the maximum number
        /// of elements is reached, overwriting the oldest element
        /// </summary>
        /// <param name="element">The element to be added.</param>
        public void Append(T element)
        {
            array[nextElementIndex] = element;
            nextElementIndex = (nextElementIndex + 1) % array.Length;

            if (Count < array.Length)
                Count++;
        }

        /// <summary>
        /// Is the array empty?
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Gets the oldest element.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown when the array is empty.</exception>
        public T OldestElement
        {
            get
            {
                if (IsEmpty)
                    throw new IndexOutOfRangeException("The array is current empty.");

                return this[0];
            }
        }

        /// <summary>
        /// Gets the latest added element.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown when the array is empty.</exception>
        public T LatestElement
        {
            get
            {
                if (IsEmpty)
                    throw new IndexOutOfRangeException("The array is current empty.");

                return this[Count - 1];
            }
        }

        /// <summary>
        /// Gets/sets an element from/in the array. 0 is always the oldest element that is still in the array,
        /// [Count-1] is always the latest added element.
        /// 
        /// This method should NOT be used to add elements - used Append() for that. Only read and write elements
        /// that are already in the array.
        /// </summary>
        /// <param name="i">The index. 0 is the oldest, [Count-1] the newest element.</param>
        /// <returns>The accessed elements.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when accessing an element outside of [0..Count-1].</exception>
        public T this[int i]
        {
            get
            {
                if ((i < 0) || (i >= Count))
                    throw new IndexOutOfRangeException("Index " + i + " (current count: " + Count + ") is out of range.");

                i = (i + nextElementIndex - Count + array.Length) % array.Length;
                return array[i];
            }

            set
            {
                if ((i < 0) || (i >= Count))
                    throw new IndexOutOfRangeException("Index " + i + " (current count: " + Count + ") is out of range.");

                i = (i + nextElementIndex - Count + array.Length) % array.Length;
                array[i] = value;
            }
        }

        /// <summary>
        /// Clears the RollingArray.
        /// </summary>
        public void Clear()
        {
            nextElementIndex = 0;
            Count = 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection from oldest to newest element.
        /// 
        /// Caveat: Due to the outdated Mono version used in Unity3D, this allocates a small amount of
        /// memory, leading to memory fragmentation if called often. You might want to use array[i] instead.
        /// 
        /// Read more about this under "Should you avoid foreach loops?" at:
        /// http://www.gamasutra.com/blogs/WendelinReich/20131109/203841/C_Memory_Management_for_Unity_Developers_part_1_of_3.php
        /// </summary>
        /// <returns>An enumerator that iterates through the collection from oldest to newest element.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty)
                yield break;

            var index = nextElementIndex - Count;
            if (index < 0)
                index += array.Length;

            for (int counter = 0; counter < Count; counter++)
            {
                yield return array[index];

                index++;
                if (index >= array.Length)
                    index -= array.Length;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection from oldest to newest element.
        /// 
        /// Caveat: Due to the outdated Mono version used in Unity3D, this allocates a small amount of
        /// memory, leading to memory fragmentation if called often. You might want to use array[i] instead.
        /// 
        /// Read more about this under "Should you avoid foreach loops?" at:
        /// http://www.gamasutra.com/blogs/WendelinReich/20131109/203841/C_Memory_Management_for_Unity_Developers_part_1_of_3.php
        /// </summary>
        /// <returns>An enumerator that iterates through the collection from oldest to newest element.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}