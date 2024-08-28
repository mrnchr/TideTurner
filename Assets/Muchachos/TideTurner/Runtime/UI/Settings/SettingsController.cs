using Muchachos.TideTurner.Runtime.Configuration;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class SettingsController : IInitializable, ISettingsController
    {
        private const float Max = 20;//1f;
        private const float Min = -80;//0.0001f;
        
        private readonly SettingsView _view;
        private readonly SettingsData _settings;
        private readonly IAudioMixerProvider _mixerProvider;

        public SettingsController(SettingsView view, SettingsData settings, IAudioMixerProvider mixerProvider)
        {
            _view = view;
            _settings = settings;
            _mixerProvider = mixerProvider;

            _view.SetController(this);
        }

        public void Initialize()
        {
            _view.SetMouseX(_settings.MouseSensitivity);
            _view.SetMusicVolume(_settings.MusicVolume);
            _view.SetSoundVolume(_settings.SoundVolume);
            
            UpdateMusicVolume(-30f);
            UpdateSoundVolume(_settings.SoundVolume);
        }

        public void UpdateMouseX(float value)
        {
            _settings.MouseSensitivity = value;
        }

        public void UpdateMusicVolume(float value)
        {
            _settings.MusicVolume = Mathf.Clamp(value, Min, Max);
            SetVolume(Idents.AudioMixerParameters.MUSIC_VOLUME, _settings.MusicVolume);
        }

        public void UpdateSoundVolume(float value)
        {
            _settings.SoundVolume = Mathf.Clamp(value, Min, Max);
            SetVolume(Idents.AudioMixerParameters.SOUND_VOLUME, _settings.MusicVolume);
        }

        private void SetVolume(string volume, float value)
        {
            _mixerProvider.Mixer.SetFloat(volume, value); //Mathf.Log10(value) * 20);
        }
    }
}