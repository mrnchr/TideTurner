using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class LoseLevelState : LevelStateBase
    {
        private readonly LevelFreezer _freezer;
        private readonly LoseWindow _lose;

        [Inject]
        public LoseLevelState(LevelFreezer levelFreezer, LoseWindow loseWindow)
        {
            _freezer = levelFreezer;
            _lose = loseWindow;
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