using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private GameObject _pauseWindow;

    private InputController _input;
    private LevelStateMachine _machine;
    private bool _isPause;

    public void Construct()
    {
        _input = FindAnyObjectByType<InputController>();
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _input.OnInputHandled += HandleInput;
    }

    private void OnDestroy()
    {
        _input.OnInputHandled -= HandleInput;
    }

    private void HandleInput(InputData data)
    {
        if (data.IsPause)
            Pause(!_isPause);
    }

    public void Pause(bool value)
    {
        if (_isPause == value)
            return;

        _isPause = value;
        if (_isPause)
        {
            _machine.ChangeState<StopLevelState>();
        }
        else
        {
            _machine.ChangeState<StayLevelState>();
        }

        _pauseWindow.SetActive(_isPause);
    }
}