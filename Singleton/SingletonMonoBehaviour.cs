using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that should only exist once and provides easy access to its instance. However, it isn't automatically
    /// created and still needs to be attached to a GameObject in the scene.
    /// </summary>
    /// <typeparam name="TSubclass">The subclass that extends from this class.</typeparam>
    public class SingletonMonoBehaviour<TSubclass> : MonoBehaviour where TSubclass : SingletonMonoBehaviour<TSubclass>
    {
        static TSubclass instance;

        /// <summary>
        /// Returns the sole instance of this class in the current scene.
        /// If the instance is not yet found cached, it searches for it and then caches it.
        /// </summary>
        public static TSubclass Instance
        {
            get
            {
                if (instance == null)
                {
                    UpdateInstance(false);
                }

                return instance;
            }
        }

        /// <summary>
        /// Returns the sole instance of this class in the current scene.
        /// If the instance is not already cached, it searches for it and then caches it.
        /// Note that if the instance is not found at all, InstanceOptional will try to
        /// find it on every call. For that reason, it is better to only call this once
        /// per script in Awake() or Start() and cache the result.
        /// </summary>
       public static TSubclass InstanceOptional
        {
            get
            {
                if (instance == null)
                {
                    // If the instance is not in the current scene, UpdateInstance will be called on every call.
                    // I could fix this by introducting a static "instanceUpdated" boolean, but then I would
                    // need to set instanceUpdated to false in OnDestroy() (for scene changes) and I don't want
                    // to introduce the complexity of virtual MonoBehaviour event methods to this class.
                    UpdateInstance(true);
                }

                return instance;
            }
        }

        /// <summary>
        /// Searches for the instance and fills <see cref="instance"/> with it. Outputs an error message if there is no
        /// instance (unless the <see cref="optional"/> parameter is set) or if there are too many instances.
        /// </summary>
        /// <param name="optional">If this is set to false, and error message will be shown if there is no instance found.</param>
        static void UpdateInstance(bool optional)
        {
            var instances = FindObjectsOfType<TSubclass>();
            if (instances.Length == 1)
            {
                instance = instances[0];
            }
            else if (instances.Length == 0)
            {
                if (!optional)
                {
                    Debug.LogError("Requested singleton of type " + typeof (TSubclass).Name + " not found.");
                }
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