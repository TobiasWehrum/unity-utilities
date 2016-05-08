using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtilities
{
    /* The SingletonMusicManager in the following example can be accessed in the same way, but it is not destroyed when the scenes switches.

       You could drop this SingletonMusicManager in multiple scenes that you work on. If at any time there are two SingletonMusicManager, the one from the previous
       scene survives and the new one is destroyed. (For that reason, you should never use SingletonMusicManager.Awake(). Instead, use OnPersistentSingletonAwake()
       because it is only called on "surviving" instances.)

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

        public void PlayMusic()
        {
            // Start playing
        }

        public void StopMusic()
        {
            // Stop playing
        }

        public void FadeToRandomSong()
        {
            // Fade to a random song
        }
    }
}
