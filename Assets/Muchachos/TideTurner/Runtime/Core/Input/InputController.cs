using System;
using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Mobile;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        [ShowInInspector]
        [HideReferencePicker]
        [ReadOnly]
        public InputData Data { get; } = new InputData();
        public event Action<InputData> OnInputHandled;

        [Range(1, 10)] [SerializeField] private float movementSmoothness = 3f;

        private MobileMoon _mobileMoon;
        private ScreenOrientation _currentOrientation, _lastOrientation;
        private SettingsData _settings;

        [Inject]
        public void Construct(SettingsData settings)
        {
            _settings = settings;
            _mobileMoon = FindAnyObjectByType<MobileMoon>();

            DeviceType platform = Application.isMobilePlatform ? DeviceType.Handheld : DeviceType.Desktop;

            switch (platform)
            {
                case DeviceType.Handheld:
                    UnityEngine.Input.gyro.enabled = true;
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
            Data.IsPause = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
        
            if (Data.IsPause)
                return;

            Data.HorizontalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            Data.VerticalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }

        private void HandleMobileInputs(InputData data)
        {
            if (Data.IsPause)
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
                    Data.HorizontalInput = UnityEngine.Input.acceleration.x / movementSmoothness;
                    break;
                default:
                    _currentOrientation = ScreenOrientation.LandscapeLeft;
                    Data.HorizontalInput = UnityEngine.Input.acceleration.y / movementSmoothness;
                    break;
            }
        }

        public void ChangePauseState()
        {
            Data.IsPause = !Data.IsPause;
        }

        public void ClearInput()
        {
            Data.Reset();
        }
    }
}