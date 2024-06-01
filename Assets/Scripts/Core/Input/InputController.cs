using System;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    public InputData Data { get; } = new InputData();
    public event Action<InputData> OnInputHandled;

    [Range(1, 10)] [SerializeField] private float movementSmoothness = 3f;

    private SettingData _settings;
    private MobileMoon _mobileMoon;
    private ScreenOrientation _currentOrientation, _lastOrientation;

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

    public void HandleInput()
    {
        if (Data.IsPause)
            ClearInput();

        OnInputHandled?.Invoke(Data);
    }

    private void HandlePCInputs(InputData data)
    {
        if (Data.IsPause || Data.Platform != DeviceType.Desktop)
            return;

        Data.IsPause = Input.GetKeyDown(KeyCode.Escape);

        Data.HorizontalInput = Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
        Data.VerticalInput = Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
    }

    private void HandleMobileInputs(InputData data)
    {
        if (Data.IsPause == true || Data.Platform != DeviceType.Handheld)
            return;

        Data.VerticalInput = _mobileMoon.slider.value;

        _currentOrientation = Screen.orientation;

        if (_currentOrientation != _lastOrientation)
        {
            Data.HorizontalInput = 0;
        }

        _lastOrientation = _currentOrientation;

        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                _currentOrientation = ScreenOrientation.Portrait;
                Data.HorizontalInput = Input.acceleration.x / movementSmoothness;
                break;
            default:
                _currentOrientation = ScreenOrientation.LandscapeLeft;
                Data.HorizontalInput = Input.acceleration.y / movementSmoothness;
                break;
        }
    }

    public void ChangePauseState()
    {
        Data.IsPause = !Data.IsPause;
    }

    public void ClearInput()
    {
        Data.HorizontalInput = 0;
        Data.VerticalInput = 0;
    }
}

/*
Vector3 deviceUpward = deviceRotation * -Vector3.forward;
Vector3 deviceForward = deviceRotation * Vector3.up;
Vector3 deviceRight = deviceRotation * Vector3.right;

//deviceForward.x = 0;
//deviceForward.y = 0;
//deviceUpward.z = 0;
*/

/*
        Quaternion referenceRotation = Quaternion.Euler(HalfPI, 0f, 0f);
        Quaternion adjustedRotation = referenceRotation * deviceRotation;

        float x = adjustedRotation.x;
        float y = adjustedRotation.y;
        float z = adjustedRotation.z;
        float w = adjustedRotation.w;

        float angleZ = Mathf.Atan2(2 * (w * z + x * y), 1 - 2 * (z * z + y * y)) * Mathf.Rad2Deg;
        angleZ /= HalfPI;
        angleZ = Mathf.Clamp(angleZ, -1f, 1f);

        Data.HorizontalInput = angleZ;
                */