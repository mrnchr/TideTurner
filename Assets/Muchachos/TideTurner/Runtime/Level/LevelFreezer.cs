using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelFreezer : MonoBehaviour
    {
        private ILevelUpdater _levelUpdater;
        private SoundRestarter _soundRestarter;

        [Inject]
        public void Construct(ILevelUpdater updater, SoundRestarter sound)
        {
            _levelUpdater = updater;
            _soundRestarter = sound;
        }

        public void Freeze()
        {
            if (!Application.isMobilePlatform) 
                Cursor.lockState = CursorLockMode.Confined;
        
            Time.timeScale = 0;
            _levelUpdater.SetPause(true);
            _soundRestarter.CachePlayedSound();
            _soundRestarter.SetSoundAllPlayed(SoundState.Pause);
        }

        public void Unfreeze()
        {
            Time.timeScale = 1;
            _levelUpdater.SetPause(false);
            _soundRestarter.SetSoundAllPlayed(SoundState.Play);
        }
    }
}