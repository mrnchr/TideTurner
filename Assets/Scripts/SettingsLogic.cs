using UnityEngine;
using UnityEngine.UI;

public class SettingsLogic : MonoBehaviour
{
    [SerializeField] private Slider mouseSliderX;
    [SerializeField] private Slider mouseSliderY;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private SettingData _settingData;
    private void Start()
    {
        _settingData = FindObjectOfType<SettingData>();

        mouseSliderX.onValueChanged.AddListener( (x) => UpdateMouseValueX(x) );
        mouseSliderY.onValueChanged.AddListener((x) => UpdateMouseValueY(x));

        musicVolumeSlider.onValueChanged.AddListener((x) => UpdateMusicVolume(x));
        soundVolumeSlider.onValueChanged.AddListener((x) => UpdateSoundVolume(x));
    }

    private void UpdateMouseValueX(float value)
    {
        _settingData.MouseSensivityX = value;

        Debug.Log(_settingData.MouseSensivityX);
    }

    private void UpdateMouseValueY(float value)
    {
        _settingData.MouseSensivityY = value;
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
