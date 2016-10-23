using UnityEngine;

namespace UnityUtilities.Examples
{
    /* The SingletonMusicManager in the following example can be accessed in the same way, but it is not destroyed when the scenes switches.

       You could make this SingletonMusicManager a prefab and drop it in multiple scenes that you work on. If at any time there are two `SingletonMusicManager`,
       the one from the previous scene survives and the new one is destroyed. (For that reason, you should never create an Awake() method in a
       PersistentSingletonMonoBehaviour. Instead, use `OnPersistentSingletonAwake()` because it is only called on "surviving" instances. Similarily, you shouldn't
       have an OnDestroy() method which would be called if this is ever destroyed via Destroy(); instead, use OnPersistentSingletonDestroyed().)

       Note that SingletonMusicManager.Instance is only available after SingletonMusicManager.Awake() was called, so if you need it in another Awake()
       call, you should put the SingletonMusicManager higher in the Script Execution Order: http://docs.unity3d.com/Manual/class-ScriptExecution.html.
    */

    public class SingletonMusicManager : PersistentSingletonMonoBehaviour<SingletonMusicManager>
    {
        protected override void OnPersistentSingletonAwake()
        {
            base.OnPersistentSingletonAwake();

            // Start playing the music the first time Awake() is called
            PlayMusic();
        }

        protected override void OnSceneSwitched()
        {
            base.OnSceneSwitched();

            // Fade to random song once a new scene is loaded
            FadeToRandomSong();
        }

        protected override void OnPersistentSingletonDestroyed()
        {
            base.OnPersistentSingletonDestroyed();

            // Stop the music when Destroy() was called on the active instance.
            StopMusic();
        }

        public void PlayMusic()
        {
            Debug.Log("Play music");
        }

        public void StopMusic()
        {
            Debug.Log("Stop music");
        }

        public void FadeToRandomSong()
        {
            Debug.Log("Fade to a random song");
        }
    }
}
