using System;
using DefaultNamespace;
using UnityEngine;

namespace Core
{
    public class InputController : MonoBehaviour, IInputController
    {
        private InputData _data;
        public InputData Data => _data;

        public event Action<InputData> OnInputHandled;
        
        private void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            ClearInput();
            
            _data.ClickDown = Input.GetKeyDown(KeyCode.Mouse0);
            _data.ClickHold = Input.GetKey(KeyCode.Mouse0);
            _data.ClickUp = Input.GetKeyUp(KeyCode.Mouse0);
            _data.MouseDeltaX = Input.GetAxis(Idents.InputAxis.MouseX);
            _data.WheelDelta = Input.GetAxis(Idents.InputAxis.ScrollWheel);
            
            OnInputHandled?.Invoke(_data);
        }

        public void ClearInput()
        {
            _data = new InputData();
        }
    }
}