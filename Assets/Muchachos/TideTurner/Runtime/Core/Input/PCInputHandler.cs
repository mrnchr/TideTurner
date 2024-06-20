using Muchachos.TideTurner.Runtime.Configuration;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class PCInputHandler : IInputHandler
    {
        private readonly SettingsData _settings;

        public PCInputHandler(SettingsData settings)
        {
            _settings = settings;
        }

        public void HandleInput(InputData data)
        {
            data.IsPause = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
        
            data.HorizontalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            data.VerticalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }
    }
}