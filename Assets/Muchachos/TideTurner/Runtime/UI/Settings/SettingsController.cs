using Muchachos.TideTurner.Runtime.Configuration;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class SettingsController : IInitializable, ISettingsController
    {
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
            
            UpdateSoundVolume(_settings.SoundVolume);
        }

        public void UpdateMouseX(float value)
        {
            _settings.MouseSensitivity = value;
        }

        public void UpdateMusicVolume(float value)
        {
            _settings.MusicVolume = Mathf.Clamp(value, Idents.AudioParameters.MinVolume, Idents.AudioParameters.MaxVolume);
            SetVolume(Idents.AudioMixerParameters.MUSIC_VOLUME, _settings.MusicVolume);
        }

        public void UpdateSoundVolume(float value)
        {
            _settings.SoundVolume = Mathf.Clamp(value, Idents.AudioParameters.MinVolume, Idents.AudioParameters.MaxVolume);
            SetVolume(Idents.AudioMixerParameters.SOUND_VOLUME, _settings.SoundVolume);
        }

        private void SetVolume(string volume, float value)
        {
            _mixerProvider.Mixer.SetFloat(volume, value);
        }
    }
}