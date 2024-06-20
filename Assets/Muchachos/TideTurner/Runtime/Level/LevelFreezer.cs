using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelFreezer : MonoBehaviour
    {
        private ILevelUpdater _updater;
        private SoundRestarter _sound;

        [Inject]
        public void Construct(ILevelUpdater updater)
        {
            _updater = updater;
        }

        public void Construct(SoundRestarter sound)
        {
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