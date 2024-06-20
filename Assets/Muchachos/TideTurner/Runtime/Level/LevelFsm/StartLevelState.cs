using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class StartLevelState : LevelStateBase
    {
        private readonly LevelStateMachine _machine;
        private readonly Level _level;
        private readonly LevelMusic _music;

        public StartLevelState(LevelStateMachine machine)
        {
            _machine = machine;
            _level = Object.FindAnyObjectByType<Level>();
            _music = Object.FindAnyObjectByType<LevelMusic>();
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