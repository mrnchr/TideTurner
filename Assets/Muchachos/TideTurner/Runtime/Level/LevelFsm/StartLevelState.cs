using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class StartLevelState : LevelStateBase
    {
        private readonly LevelStateMachine _machine;
        private readonly Level _level;
        private readonly LevelMusic _music;

        [Inject]
        public StartLevelState(LevelStateMachine machine, Level level, LevelMusic levelMusic)
        {
            _machine = machine;
            _level = level;
            _music = levelMusic;
        }

        public override void Enter()
        {
            _level.Init();
            _music.SetSound(SoundState.Play);
            _machine.ChangeState<StayLevelState>();
        }

        public override void Exit()
        {
        }
    }
}