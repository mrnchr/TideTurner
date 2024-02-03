using UnityEngine;

public class PauseLevelState : LevelStateBase
{
    private readonly Level _level;
    private readonly InputController _input;

    public PauseLevelState(LevelStateMachine machine) : base(machine)
    {
        _level = Object.FindAnyObjectByType<Level>();
        _input = Object.FindAnyObjectByType<InputController>();
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _input.SetPause(true);
        _level.SetPause(true);
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        _input.SetPause(false);
        _level.SetPause(false);
    }
}