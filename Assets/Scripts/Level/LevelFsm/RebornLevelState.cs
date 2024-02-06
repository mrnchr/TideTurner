using UnityEngine;

public class RebornLevelState : LevelStateBase
{
    private readonly LevelMusic _music;
    private readonly Level _level;

    public RebornLevelState(LevelStateMachine machine) : base(machine)
    {
        _level = Object.FindAnyObjectByType<Level>();
        _music = Object.FindAnyObjectByType<LevelMusic>();
    }

    public override void Enter()
    {
        _level.Reborn();
        _music.SetSound(SoundState.Play);
        _machine.ChangeState<StayLevelState>();
    }

    public override void Exit()
    {
    }
}