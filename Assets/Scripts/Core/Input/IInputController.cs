using System;

public interface IInputController
{
    public InputData Data { get; }
    public event Action<InputData> OnInputHandled;
        
    public void HandleInput();
    public void ClearInput();
}