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
        private CameraMovement _cameraMovement;
        private ScreenOrientation _temp;
        private IInputController _input;

        [Inject]
        public void Construct(IInputController input)
        {
            _input = input;
            _input.OnInputHandled += UpdateLogic;
        }

        public void Construct(AbstractMoonData data, CameraMovement cameraMovement)
        {
            _data = data;
            _cameraMovement = cameraMovement;
        
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
    
        private void HandleOrientationData()
        {
            if (Screen.orientation != _temp)
            {
                OnScreenOrientationChange?.Invoke();
            }

            _cameraMovement.ChangeOrthographicSize(
                Screen.orientation == ScreenOrientation.Portrait
                    ? OrthographicSizeType.PORTAIT
                    : OrthographicSizeType.NORMAL);

            _temp = Screen.orientation;
        }
    }
}
