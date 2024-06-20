using System;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public interface IInputController
    {
        public InputData Data { get; }
        bool IsPaused { get; set; }
        public event Action<InputData> OnInputHandled;
        void HandleInput();
        void ClearInput();
    }
}