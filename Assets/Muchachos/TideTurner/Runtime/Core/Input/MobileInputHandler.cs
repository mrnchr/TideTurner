using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Debugging;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class MobileInputHandler : IInputHandler
    {
        private readonly RawMobileInputData _rawInputData;
        private readonly ILoggerController _logger;
        private readonly MobileConfig _config;
        
        private ScreenOrientation _currentOrientation;
        private ScreenOrientation _lastOrientation;
        
        public MobileInputHandler(IConfigProvider configProvider, RawMobileInputData rawInputData, ILoggerController logger)
        {
            _rawInputData = rawInputData;
            _logger = logger;
            _config = configProvider.Get<MobileConfig>();
        }

        public void HandleInput(InputData data)
        {
            data.VerticalInput = _rawInputData.VerticalMovement;

            _currentOrientation = Screen.orientation;

            if (_currentOrientation != _lastOrientation)
            {
                data.HorizontalInput = 0;
            }

            _lastOrientation = _currentOrientation;
            
            data.HorizontalInput = UnityEngine.Input.acceleration.x / _config.MovementSmoothness;
            _currentOrientation = Screen.orientation;
            
            _logger.Log(data.HorizontalInput);
            _logger.Log(data.VerticalInput);
        }
    }
}