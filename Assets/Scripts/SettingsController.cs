using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider mouseSliderX;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    [SerializeField] private Slider brightness;

    private SettingData _settingData;

    public void Construct()
    {
        _settingData = FindFirstObjectByType<SettingData>();
    }

    public void Init()
    {
        mouseSliderX.value = _settingData.MouseSensitivity;
        musicVolumeSlider.value = _settingData.MusicVolume;
        soundVolumeSlider.value = _settingData.SoundVolume;
        
        mouseSliderX.onValueChanged.AddListener(UpdateMouseValueX);
        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(UpdateSoundVolume);
    }

    private void UpdateMouseValueX(float value)
    {
        _settingData.MouseSensitivity = value;
    }

    private void UpdateMusicVolume(float value)
    {
        _settingData.MusicVolume = value;
    }

    private void UpdateSoundVolume(float value)
    {
        _settingData.SoundVolume = value;
    }
}