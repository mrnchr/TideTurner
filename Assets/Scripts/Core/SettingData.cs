using UnityEngine;
using UnityEngine.Audio;

public class SettingData : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    
    [SerializeField] private float _defaultMouseSensitivity = 1;
    [Range(0.0001f, 1)][SerializeField] private float _defaultMusicVolume = 0.5f;
    [Range(0.0001f, 1)][SerializeField] private float _defaultSoundVolume = 0.5f;
    
    [Header("Runtime")]
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _soundVolume;

    public float MouseSensitivity
    {
        get => _mouseSensitivity;
        set => _mouseSensitivity = value;
    }

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            if (_musicVolume == value)
                return;
            
            _musicVolume = Mathf.Clamp(value, 0.0001f, 1);
            SetVolume(Idents.AudioMixerParameters.MUSIC_VOLUME, _musicVolume);
        }
    }

    public float SoundVolume
    {
        get => _soundVolume;
        set
        {
            if (_soundVolume == value)
                return;
            
            _soundVolume = Mathf.Clamp(value, 0.0001f, 1);
            SetVolume(Idents.AudioMixerParameters.SOUND_VOLUME, _soundVolume);
        }
    }

    public void Init()
    {
        MusicVolume = _defaultMusicVolume;
        SoundVolume = _defaultSoundVolume;
        MouseSensitivity = _defaultMouseSensitivity;
    }

    private void SetVolume(string volume, float value)
    {
        _mixer.SetFloat(volume, Mathf.Log10(value) * 20);
    }
}