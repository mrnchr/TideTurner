using Muchachos.TideTurner.Runtime.Configuration;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class PCInputHandler : IInputHandler
    {
        private readonly SettingsData _settings;
        private readonly ApplicationFocusHandler _applicationFocusHandler;
        private bool _temp;

        [Inject]
        public PCInputHandler(SettingsData settings, ApplicationFocusHandler applicationFocusHandler)
        {
            _settings = settings;
            _applicationFocusHandler = applicationFocusHandler;
        }

        public void HandleInput(InputData data)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || _temp)
            {
                data.IsPause = !data.IsPause;
                _temp = false;
            }
            
            if (!_applicationFocusHandler.IsFocused)
            {
                data.IsPause = true;

                _temp = true;
                Debug.Log("IsFocused: " + _applicationFocusHandler.IsFocused);
            }
        
            data.HorizontalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            data.VerticalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }
    }
}