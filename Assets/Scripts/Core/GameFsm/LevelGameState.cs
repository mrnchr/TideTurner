using System.Linq;
using UnityEngine;

public class LevelGameState : GameStateBase
{
    private MusicPlayer _levelMusic;
    public LevelGameState(GameStateMachine machine) : base(machine)
    {
        _levelMusic = Object.FindObjectsOfType<MusicPlayer>().Where(x => x.MusicType == MusicType.level).FirstOrDefault();
    }

    public override void Enter()
    {
        _levelMusic.SetState(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Exit()
    {
        _levelMusic.SetState(false);
    }
}