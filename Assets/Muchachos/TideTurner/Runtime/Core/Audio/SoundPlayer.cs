using System;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private bool isSound3D;

        public bool IsPlaying => _sound.isPlaying;

        private AudioSource _sound;
        private YandexGamesIntegration _yandexGamesIntegration;

        private Action _cachedStart;

        [Inject]
        public void Construct(YandexGamesIntegration yandexGamesIntegration)
        {
            _yandexGamesIntegration = yandexGamesIntegration;

            _cachedStart += () => SetSoundState(SoundState.Stop);

            _yandexGamesIntegration.OnAdv += _cachedStart.Invoke;
        }

        private void Awake()
        {
            _sound = GetComponent<AudioSource>();
            _sound.spatialBlend = isSound3D ? 1 : 0;
        }

        public void SetSoundState(SoundState state)
        {
            switch (state)
            {
                case SoundState.Play:
                    _sound.Play();
                    break;
                case SoundState.Pause:
                    _sound.Pause();
                    break;
                case SoundState.Stop:
                    _sound.Stop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnDestroy()
        {
            _yandexGamesIntegration.OnAdv -= _cachedStart.Invoke;
        }
    }

    public enum SoundState
    {
        Play = 0,
        Pause = 1,
        Stop = 2
    }
}