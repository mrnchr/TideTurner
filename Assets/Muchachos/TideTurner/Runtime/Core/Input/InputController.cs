using System;
using TriInspector;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class InputController : IInputController, ITickable
    {
        private readonly IInputHandler _handler;
        private readonly ApplicationFocusHandler _applicationFocusHandler;

        public event Action<InputData> OnInputHandled;

        [ShowInInspector]
        [HideReferencePicker]
        [ReadOnly]
        public InputData Data { get; } = new InputData();

        public bool IsPaused { get; set; }
        
        private bool _wasntFocused;

        public InputController(IInputHandler handler, ApplicationFocusHandler applicationFocusHandler)
        {
            _handler = handler;
            _applicationFocusHandler = applicationFocusHandler;
        }

        public void Tick()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            _handler.HandleInput(Data);

            HandleFocus();

            if (IsPaused)
            {
                Data.HorizontalInput = 0;
                Data.VerticalInput = 0;
                //bool paused = Data.IsPause;
                //ClearInput();
                //Data.IsPause = paused;
            }

            OnInputHandled?.Invoke(Data);
        }

        private void HandleFocus()
        {
            if (_wasntFocused)
            {
                Data.IsPause = !Data.IsPause;
                _wasntFocused = false;
            }
            
            if (_applicationFocusHandler.IsFocused)
                return;
            
            Data.IsPause = true;

            _wasntFocused = true;
        }

        public void ClearInput()
        {
            Data.Reset();
        }
    }
}