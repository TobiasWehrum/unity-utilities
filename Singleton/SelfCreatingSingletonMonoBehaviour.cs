using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that should only exist once and provides easy access to its instance. If none exists in
    /// the secene at the time it is requested, it will be automatically created.
    /// </summary>
    /// <typeparam name="TSubclass">The subclass that extends from this class.</typeparam>
    public class SelfCreatingSingletonMonoBehaviour<TSubclass> : MonoBehaviour where TSubclass : SelfCreatingSingletonMonoBehaviour<TSubclass>
    {
        static TSubclass instance;

        /// <summary>
        /// Returns the sole instance of this class in the current scene.
        /// If the instance is not yet found cached, it searches for it and then caches it.
        /// If no instance is found, it creates one.
        /// </summary>
        public static TSubclass Instance
        {
            get
            {
                if (instance == null)
                {
                    UpdateInstance();
                }

                return instance;
            }
        }


        /// <summary>
        /// Searches for the instance and fills <see cref="instance"/> with it. Creates an instance if there is none and
        /// outputs an error message if there are too many instances.
        /// </summary>
        static void UpdateInstance()
        {
            var instances = FindObjectsOfType<TSubclass>();
            if (instances.Length == 1)
            {
                instance = instances[0];
            }
            else if (instances.Length == 0)
            {
                instance = new GameObject(typeof(TSubclass).Name).AddComponent<TSubclass>();
            }
            else
            {
                Debug.LogError("Requested singleton of type " + typeof (TSubclass).Name + " has " + instances.Length + "instances.");
            }
        }

        /// <summary>
        /// If this is the instance and it was destroyed,
        /// remove the instance registration.
        /// 
        /// Note: This also means that you need to use
        /// 
        ///     protected override void OnDestroy()
        ///     {
        ///         base.OnDestroy();
        ///         // [Your code]
        ///     }
        /// 
        /// in subclasses.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}