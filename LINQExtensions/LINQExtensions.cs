using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A collection of extension methods for <see cref="IEnumerable{T}"/>, <see cref="List{T}"/> and arrays.
    /// </summary>
    public static class LINQExtensions
    {
        /// <summary>
        /// Takes a collection, generates values from the items and and returns the item with the lowest generated value.
        /// Useful for example to find the closest item. Reverse the generated values to find the item with the highest generated value.
        /// 
        /// This returns the same as list.Where(element => predicateValue(valueConverter(element))).OrderBy(valueConverter).First(), but it
        /// a) doesn't order the whole list and
        /// b) doesn't call valueConverted more than once per element.
        /// </summary>
        /// <typeparam name="TElement">The collection element type.</typeparam>
        /// <typeparam name="TValue">The generated value type.</typeparam>
        /// <param name="list">The list of elements.</param>
        /// <param name="valueConverter">The method to convert an element to a generated value used for ordering.</param>
        /// <param name="predicateValue">A predicate testing whether the generated value is permitted. If true, the element is used; if false, the element is skipped.</param>
        /// <returns>The first element by the generated value in order that succeeded the predicateValue test.</returns>
        public static TElement FirstByGeneratedValue<TElement, TValue>(this IEnumerable<TElement> list, Func<TElement, TValue> valueConverter, Predicate<TValue> predicateValue = null)
            where TValue : IComparable
        {
            var isFirstElement = true;
            TElement bestElement = default(TElement);
            TValue bestValue = default(TValue);

            // For each element...
            foreach (var element in list)
            {
                // Generate its value
                var value = valueConverter(element);

                // Check whether its value is permitted
                if ((predicateValue != null) && (!predicateValue(value)))
                    continue;

                // If it's the first permitted element or better than the previous best element...
                if (isFirstElement || (value.CompareTo(bestValue) < 0))
                {
                    // ...set it as the best element
                    isFirstElement = false;
                    bestElement = element;
                    bestValue = value;
                }
            }

            return bestElement;
        }

        /// <summary>
        /// Returns the component nearest to the referencePoint and in minimum/maximum range.
        /// </summary>
        /// <typeparam name="TElement">The collection element type. Needs to be a subclass of <see cref="Component"/>.</typeparam>
        /// <param name="list">The list of components.</param>
        /// <param name="referencePoint">A reference point to get the distance from.</param>
        /// <param name="minDistance">Optional: The minimum distance.</param>
        /// <param name="maxDistance">Optional: The maximum distance.</param>
        /// <returns>The element nearest to the referencePoint and in minimum/maximum range.</returns>
        public static TElement Nearest<TElement>(this IEnumerable<TElement> list, Vector3 referencePoint, float minDistance = 0, float maxDistance = float.PositiveInfinity)
            where TElement : Component
        {
            // Create the predicate value from the min/max distance.
            Predicate<float> predicateValue = null;
            if ((minDistance != 0) || !float.IsPositiveInfinity(maxDistance))
            {
                var minDistanceSq = minDistance * minDistance;
                var maxDistanceSq = maxDistance * maxDistance;
                predicateValue = (distanceSq => (distanceSq >= minDistanceSq) && (distanceSq <= maxDistanceSq));
            }

            // Return the nearest element to the reference point in the given range.
            return list.FirstByGeneratedValue(component => (component.transform.position - referencePoint).sqrMagnitude, predicateValue);
        }

        /// <summary>
        /// Shuffles an array in place.
        /// </summary>
        /// <typeparam name="T">The array element type.</typeparam>
        /// <param name="list">The array to shuffle.</param>
        public static void Shuffle<T>(this T[] list)
        {
            var count = list.Length;
            for (int i1 = 0; i1 < count; i1++)
            {
                var i2 = UnityEngine.Random.Range(0, count);
                var element = list[i1];
                list[i1] = list[i2];
                list[i2] = element;
            }
        }

        /// <summary>
        /// Shuffles a list in place.
        /// </summary>
        /// <typeparam name="T">The list element type.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(this List<T> list)
        {
            var count = list.Count;
            for (int i1 = 0; i1 < count; i1++)
            {
                var i2 = UnityEngine.Random.Range(0, count);
                var element = list[i1];
                list[i1] = list[i2];
                list[i2] = element;
            }
        }

        /// <summary>
        /// Returns a random element from the array.
        /// </summary>
        /// <typeparam name="T">The array element type.</typeparam>
        /// <param name="array">The array to return an element from.</param>
        /// <returns>A random element from the array.</returns>
        public static T RandomElement<T>(this T[] array)
        {
            var index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        /// <typeparam name="T">The list element type.</typeparam>
        /// <param name="list">The list to return an element from.</param>
        /// <returns>A random element from the list.</returns>
        public static T RandomElement<T>(this List<T> list)
        {
            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Calls ToString() on every element of the list, puts [encapsulate] directly before and after the result
        /// and then concatenates the results with [seperator] between them.
        /// </summary>
        /// <typeparam name="T">The collection element type.</typeparam>
        /// <param name="list">The collection to concatenate.</param>
        /// <param name="separator">The seperator between entries.</param>
        /// <param name="encapsulate">The string to put directly before and after every item.</param>
        /// <returns>A string containing the ToString() results of all items.</returns>
        public static string ToOneLineString<T>(this IEnumerable<T> list, string separator = ", ", string encapsulate = "\"")
        {
            var useEncapsulate = encapsulate.Length > 0;

            var result = new StringBuilder();
            foreach (var element in list)
            {
                if (result.Length > 0)
                    result.Append(separator);

                if (useEncapsulate)
                    result.Append(encapsulate);

                result.Append(element);

                if (useEncapsulate)
                    result.Append(encapsulate);
            }

            return result.ToString();
        }
    }
}