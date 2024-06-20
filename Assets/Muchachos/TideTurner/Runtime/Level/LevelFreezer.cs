using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelFreezer : MonoBehaviour
    {
        private LevelUpdater _updater;
        private SoundRestarter _sound;

        public void Construct(LevelUpdater updater, SoundRestarter sound)
        {
            _updater = updater;
            _sound = sound;
        }

        public void Freeze()
        {
            if (Application.isMobilePlatform == false) 
                Cursor.lockState = CursorLockMode.Confined;
        
            Time.timeScale = 0;
            _updater.SetPause(true);
            _sound.CachePlayedSound();
            _sound.SetSoundAllPlayed(SoundState.Pause);
        }

        public void Unfreeze()
        {
            Time.timeScale = 1;
            _updater.SetPause(false);
            _sound.SetSoundAllPlayed(SoundState.Play);
        }
    }
}