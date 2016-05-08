using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtilities
{
    // Drop an instance of this in the scene. It won't be destroyed between scenes.
    // The instance can accessed via SingletonMusicManager.Instance.
    // E.g.: SingletonMusicManager.Instance.StopMusic()

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
