using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// Any class that extends from this one will persist through scenes (via <see cref="Object.DontDestroyOnLoad"/>) and can be handily accessed
    /// via the static <see cref="Instance"/> property.
    /// <para>If a new scene is loaded with a new instance of this class while another persistent instance already exists, the new instance in the scene is destroyed.
    /// This allows you to make this a prefab and have it in all scenes that you are working on.</para>
    /// </summary>
    /// <typeparam name="TSubclass">The subclass that extends from this class.</typeparam>
    public class PersistentSingletonMonoBehaviour<TSubclass> : MonoBehaviour where TSubclass : PersistentSingletonMonoBehaviour<TSubclass>
    {
        static TSubclass instance;

        /// <summary>
        /// Returns the singleton instance.
        /// </summary>
        public static TSubclass Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Returns true if a singleton instance is registered.
        /// </summary>
        protected bool InstanceExists
        {
            get { return instance != null; }
        }

        /// <summary>
        /// Used to skip the first "OnLevelWasLoaded".
        /// </summary>
        bool skipOnLevelWasLoaded;

        /// <summary>
        /// If this is the first instance of this type:
        /// 1. Register it as the instance.
        /// 2. Mark it as <see cref="Object.DontDestroyOnLoad"/>.
        /// 3. Call <see cref="OnPersistentSingletonAwake"/>.
        /// 4. Call <see cref="OnAwakeOrSwitch"/>.
        /// 
        /// If an instance of this already exists, destroy this instance.
        /// </summary>
        void Awake()
        {
            // If an instance already exists, destroy this instance.
            if (InstanceExists)
            {
                Destroy(gameObject);
                return;
            }

            // Register, make persistent and call event methods
            instance = (TSubclass) this;
            DontDestroyOnLoad(gameObject);
            OnPersistentSingletonAwake();
            OnAwakeOrSwitch();

            // Make sure to skip "OnLevelWasLoaded" for this first Awake - 
            // we only want to go there when the scene changes.
            skipOnLevelWasLoaded = true;
        }

        /// <summary>
        /// If this is the persistent instance and it was destroyed (manually),
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
                OnPersistentSingletonDestroyed();
            }
        }

        /// <summary>
        /// Called when the scene is first loaded or was switched.
        /// </summary>
        void OnLevelWasLoaded()
        {
            // First time? Skip it - it's not a scene switch.
            if (skipOnLevelWasLoaded)
                return;

            if (Instance == this)
            {
                // Call the event methods.
                OnSceneSwitched();
                OnAwakeOrSwitch();
            }
        }

        /// <summary>
        /// Disable <see cref="skipOnLevelWasLoaded"/>.
        /// </summary>
        protected virtual void Start()
        {
            // The first "OnLevelWasLoaded" was called already; disable the skipping.
            skipOnLevelWasLoaded = false;
        }

        /// <summary>
        /// This method is called when the Awake() method of the first instance of the persistent
        /// singleton is done. This is not called if this is a second instance (which is destroyed
        /// automatically immediately).
        /// </summary>
        protected virtual void OnPersistentSingletonAwake()
        {
        }

        /// <summary>
        /// This method is called when the registered instance of the persistent singleton is either
        /// destroyed manually by calling Destroy() or the application is closed. This is not called
        /// if this is a second instance (which is destroyed automatically immediately).
        /// </summary>
        protected virtual void OnPersistentSingletonDestroyed()
        {
        }

        /// <summary>
        /// This method is called after switching to a new scene.
        /// </summary>
        protected virtual void OnSceneSwitched()
        {
        }

        /// <summary>
        /// This method is called immediately after <see cref="OnPersistentSingletonAwake"/>
        /// or <see cref="OnSceneSwitched"/>.
        /// </summary>
        protected virtual void OnAwakeOrSwitch()
        {
        }
    }
}