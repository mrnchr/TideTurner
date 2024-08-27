using System;
using Muchachos.TideTurner.Runtime.Core.Input;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileScreenOrientation : MonoBehaviour
    {
        public event Action OnScreenOrientationChange;

        private AbstractMoonData _data;
        private CameraScaler[] _cameraScalers;
        private ScreenOrientation _temp;
        private IInputController _input;

        [Inject]
        public void Construct(IInputController input, AbstractMoonData data, CameraScaler[] cameraScalers)
        {
            _input = input;
            _data = data;
            _cameraScalers = cameraScalers;
            
            _input.OnInputHandled += UpdateLogic;
            OnScreenOrientationChange += _data.Init;
        }
    
        private void OnDisable()
        {
            _input.OnInputHandled -= UpdateLogic;
            OnScreenOrientationChange -= _data.Init;
        }

        private void UpdateLogic(InputData inputData)
        {
            HandleOrientationData();

            Cursor.lockState = CursorLockMode.Confined;
        }
    
        // todo: delete?
        private void HandleOrientationData()
        {
            if (Screen.orientation != _temp)
            {
                OnScreenOrientationChange?.Invoke();
            }

            OrthographicSizeType orthographicSizeType = Screen.orientation == ScreenOrientation.Portrait
                ? OrthographicSizeType.PORTAIT
                : OrthographicSizeType.NORMAL;

            foreach (var cameraScaler in _cameraScalers)
            {
                cameraScaler.ChangeOrthographicSize(orthographicSizeType);
            }

            _temp = Screen.orientation;
        }
    }
}
