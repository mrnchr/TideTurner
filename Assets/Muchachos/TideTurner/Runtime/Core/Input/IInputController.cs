using System;

namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public interface IInputController
    {
        public InputData Data { get; }
        public event Action<InputData> OnInputHandled;
        
        public void HandleInput();
        public void ClearInput();
    }
}