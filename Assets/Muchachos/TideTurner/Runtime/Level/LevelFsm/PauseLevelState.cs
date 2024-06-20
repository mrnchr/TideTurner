using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class PauseLevelState : LevelStateBase
    {
        private readonly IInputController _input;
        private readonly LevelFreezer _freezer;
        private readonly PauseWindow _pause;

        public PauseLevelState(IInputController input)
        {
            _input = input;
            _freezer = Object.FindAnyObjectByType<LevelFreezer>();
        }

        public override void Enter()
        {
            _input.IsPaused = true;
            _freezer.Freeze();
        }

        public override void Exit()
        {
            _freezer.Unfreeze();
            _input.IsPaused = false;
        }
    }
}