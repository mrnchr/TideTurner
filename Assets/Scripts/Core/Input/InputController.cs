using System;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    public InputData Data { get; private set; } = new InputData();
    public event Action<InputData> OnInputHandled;
    public event Action OnOrientationChange;

    private bool _isPaused;
    private SettingData _settings;
    private ScreenOrientation _temp;
    public void Construct()
    {
        _settings = FindAnyObjectByType<SettingData>();
        
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Debug.Log("Windows");
        }
        
        if (Application.isMobilePlatform == true)
        {
            Input.gyro.enabled = true;
        }

        _temp = Screen.orientation;
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
        ClearInput();
            
        if (!_isPaused && Application.isMobilePlatform == false)
        {
            Data.HorizontalInput = Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            Data.VerticalInput = Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }
        
        if (!_isPaused && Application.isMobilePlatform == true)
        {
            if (Screen.orientation != _temp )
            {
                OnOrientationChange?.Invoke();
            }
            
            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                    Data.HorizontalInput = -Input.gyro.rotationRate.z;
                    Data.VerticalInput = -Input.gyro.rotationRate.x  / 10;
                    break;
                case ScreenOrientation.LandscapeLeft:
                    Data.HorizontalInput = -Input.gyro.rotationRate.z ;
                    Data.VerticalInput = Input.gyro.rotationRate.y / 10;
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    Data.HorizontalInput = Input.gyro.rotationRate.y;
                    Data.VerticalInput = -Input.gyro.rotationRate.z  / 10;
                    break;
                case ScreenOrientation.LandscapeRight:
                    Data.HorizontalInput = -Input.gyro.rotationRate.z;
                    Data.VerticalInput = Input.gyro.rotationRate.y / 10;
                    break;
                default:
                    Data.HorizontalInput = -Input.gyro.rotationRate.z;
                    Data.VerticalInput = -Input.gyro.rotationRate.x  / 10;
                    break;
            }

            _temp = Screen.orientation;
        }
        
        Data.IsPause = Input.GetKeyDown(KeyCode.Escape);

        OnInputHandled?.Invoke(Data);
    }

    public void ClearInput()
    {
        Data = new InputData();
    }
}