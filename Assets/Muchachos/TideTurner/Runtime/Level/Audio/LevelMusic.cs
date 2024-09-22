using System;
using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelMusic : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _sound;

        private YandexGamesIntegration _yandexGamesIntegration;

        private Action _cachedStart, _cachedEnd;

        [Inject]
        public void Construct(YandexGamesIntegration yandexGamesIntegration)
        {
            _yandexGamesIntegration = yandexGamesIntegration;

            _cachedStart = () => SetSound(SoundState.Pause);
            _cachedEnd = () => SetSound(SoundState.Play);
            
            _yandexGamesIntegration.OnAdv += _cachedStart.Invoke;
            _yandexGamesIntegration.OnAdvEnd += _cachedEnd.Invoke;
        }

        public void SetSound(SoundState state)
        {
            _sound.SetSoundState(state);
        }

        private void OnDestroy()
        {
            _yandexGamesIntegration.OnAdv -= _cachedStart.Invoke;
            _yandexGamesIntegration.OnAdvEnd -= _cachedEnd.Invoke;
        }
    }
}