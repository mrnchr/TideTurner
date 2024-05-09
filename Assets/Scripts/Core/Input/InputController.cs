using System;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    public InputData Data { get; } = new InputData();
    public event Action<InputData> OnInputHandled;

    private bool _isPaused;
    private SettingData _settings;
    private MobileMoon _mobileMoon;
    public void Construct()
    {
        _settings = FindAnyObjectByType<SettingData>();
        _mobileMoon = FindAnyObjectByType<MobileMoon>();
        
        Data.Platform = Application.isMobilePlatform ? DeviceType.Handheld : DeviceType.Desktop;

        switch (Data.Platform)
        {
            case DeviceType.Handheld:
                Input.gyro.enabled = true;
                OnInputHandled += HandleMobileInputs;
                break;
            case DeviceType.Desktop:
                OnInputHandled += HandlePCInputs;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        } 
    }

    private void Update()
    {
        HandleInput();
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }

    public void HandleInput()
    {
        if(_isPaused == true)
            ClearInput();
        
        Data.IsPause = Input.GetKeyDown(KeyCode.Escape);

        OnInputHandled?.Invoke(Data);
    }

    private void HandlePCInputs(InputData data)
    {
        if (_isPaused || Data.Platform != DeviceType.Desktop) 
            return;
        
        Data.HorizontalInput = Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
        Data.VerticalInput = Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
    }

    private void HandleMobileInputs(InputData data)
    {
        if (_isPaused || Data.Platform != DeviceType.Handheld) 
            return;
            
        Data.VerticalInput = _mobileMoon.slider.value;
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                Data.HorizontalInput = -Input.gyro.rotationRateUnbiased.z;
                break;
            default:
                Data.HorizontalInput = -Input.gyro.rotationRateUnbiased.z ;
                break;
        }
    }

    public void ClearInput()
    {
        Data.HorizontalInput = 0;
        Data.VerticalInput = 0;
    }
}