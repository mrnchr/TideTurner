using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class MobileInputHandler : IInputHandler
    {
        private readonly MobileConfig _config;
        private readonly MobileMoon _mobileMoon;

        private ScreenOrientation _currentOrientation;
        private ScreenOrientation _lastOrientation;
        
        [Inject]
        public MobileInputHandler(IConfigProvider configProvider, AbstractMoon mobileMoon)
        {
            _config = configProvider.Get<MobileConfig>();
            _mobileMoon = mobileMoon as MobileMoon;
        }

        public void HandleInput(InputData data)
        {
            data.VerticalInput = _mobileMoon.GetSliderValue;

            _currentOrientation = Screen.orientation;

            if (_currentOrientation != _lastOrientation)
            {
                data.HorizontalInput = 0;
            }

            _lastOrientation = _currentOrientation;

            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                    _currentOrientation = ScreenOrientation.Portrait;
                    data.HorizontalInput = UnityEngine.Input.acceleration.x / _config.MovementSmoothness;
                    break;
                default:
                    _currentOrientation = ScreenOrientation.LandscapeLeft;
                    data.HorizontalInput = UnityEngine.Input.acceleration.y / _config.MovementSmoothness;
                    break;
            }
        }
    }
}