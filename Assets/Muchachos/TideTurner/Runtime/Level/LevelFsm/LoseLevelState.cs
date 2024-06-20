using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class LoseLevelState : LevelStateBase
    {
        private readonly LevelFreezer _freezer;
        private readonly LoseWindow _lose;

        public LoseLevelState()
        {
            _freezer = Object.FindAnyObjectByType<LevelFreezer>();
            _lose = Object.FindAnyObjectByType<LoseWindow>();
        }

        public override void Enter()
        {
            _freezer.Freeze();
            _lose.SetActive(true);
        }

        public override void Exit()
        {
            _freezer.Unfreeze();
            _lose.SetActive(false);
        }
    }
}