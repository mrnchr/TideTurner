using Muchachos.TideTurner.Runtime.Configuration;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class PCInputHandler : IInputHandler
    {
        private readonly SettingsData _settings;

        [Inject]
        public PCInputHandler(SettingsData settings)
        {
            _settings = settings;
        }

        public void HandleInput(InputData data)
        {
            if (UnityEngine.Input.GetKeyDown(GlobalVariables.Escape))
            {
                data.IsPause = !data.IsPause;
            }
        
            data.HorizontalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            data.VerticalInput = UnityEngine.Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }
    }
}