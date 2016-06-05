using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityUtilities
{
    /// <summary>
    /// Provides a fluidly changing output value using <see cref="Mathf.PerlinNoise"/>.
    /// </summary>
    [Serializable]
    public class NoiseOutputValue
    {
        /// <summary>
        /// The range of the output value.
        /// </summary>
        [Tooltip("The range of the output value.")]
        [SerializeField] RangeFloat range = new RangeFloat(0f, 1f);

        /// <summary>
        /// How fast to scroll over the perlin noise.
        /// </summary>
        [Tooltip("How fast to scroll over the perlin noise.")]
        [SerializeField] float speed = 0.1f;

        /// <summary>
        /// Has the random seed already been initialized?
        /// </summary>
        bool initialized;

        /// <summary>
        /// The scrolling time used as x for the 2d perlin function.
        /// </summary>
        float perlinTime;

        /// <summary>
        /// The seed used as y for the 2d perlin function. This scrolls too, but very slowly, to avoid early looping.
        /// </summary>
        float perlinSeed;

        /// <summary>
        /// The current output value between range.from and range.to.
        /// </summary>
        public float OutputValue { get; private set; }

        /// <summary>
        /// The range of the output value.
        /// </summary>
        public RangeFloat Range { get { return range; } }

        /// <summary>
        /// The range of the output speed.
        /// </summary>
        public float Speed { get { return speed; } }

        /// <summary>
        /// Updates the <see cref="OutputValue"/>. Should be called once per frame before using <see cref="OutputValue"/> for the first time.
        /// </summary>
        /// <param name="deltaTime">Optional: Provide the deltaTime for the scrolling if it isn't Time.deltaTime.</param>
        public void Progress(float? deltaTime = null)
        {
            if (!initialized)
            {
                initialized = true;

                // Gets a random seed to use as y for the 2d perlin function.
                perlinSeed = Random.value * 10f;
            }

            if (deltaTime == null)
                deltaTime = Time.deltaTime;

            var delta = deltaTime.Value * speed;

            // Scroll forward in time.
            perlinTime += delta;

            // Scroll very slowing through the seed to avoid looping at PerlinTime values of 10 and higher.
            // It will still loop, but only after 
            perlinSeed += delta / 100f;

            // Update the output value.
            OutputValue = range.Lerp(Mathf.PerlinNoise(perlinTime, perlinSeed));
        }
   }
}