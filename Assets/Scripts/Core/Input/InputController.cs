using System;
using DefaultNamespace;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    [Header("Runtime")]
    [SerializeField] private InputData _data;
    private bool _isPaused;
    private SettingData _settings;

    public InputData Data => _data;

    public event Action<InputData> OnInputHandled;

    public void Construct()
    {
        _settings = FindAnyObjectByType<SettingData>();
    }

    private void Update()
    {
        HandleInput();
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }

    public void HandleInput()
    {
        ClearInput();

        if (!_isPaused)
        {
            _data.ClickDown = Input.GetKeyDown(KeyCode.Mouse0);
            _data.ClickHold = Input.GetKey(KeyCode.Mouse0);
            _data.ClickUp = Input.GetKeyUp(KeyCode.Mouse0);
            _data.MouseDeltaX = Input.GetAxis(Idents.InputAxis.MOUSE_X) * _settings.MouseSensitivity;
            _data.WheelDelta = Input.GetAxis(Idents.InputAxis.SCROLL_WHEEL);
        }

        _data.IsPause = Input.GetKeyDown(KeyCode.Escape);
            
        OnInputHandled?.Invoke(_data);
    }

    public void ClearInput()
    {
        _data = new InputData();
    }
}