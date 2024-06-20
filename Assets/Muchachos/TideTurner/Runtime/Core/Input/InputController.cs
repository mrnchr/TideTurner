using System;
using TriInspector;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class InputController : IInputController, ITickable
    {
        private readonly IInputHandler _handler;

        public event Action<InputData> OnInputHandled;

        [ShowInInspector]
        [HideReferencePicker]
        [ReadOnly]
        public InputData Data { get; } = new InputData();
        
        public bool IsPaused { get; set; }

        public InputController(IInputHandler handler)
        {
            _handler = handler;
        }

        public void Tick()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            _handler.HandleInput(Data);
            
            if (IsPaused)
            {
                bool paused = Data.IsPause;
                ClearInput();
                Data.IsPause = paused;
            }

            OnInputHandled?.Invoke(Data);
        }

        public void ClearInput()
        {
            Data.Reset();
        }
    }
}