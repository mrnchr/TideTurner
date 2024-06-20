using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Shark;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class RebornLevelState : LevelStateBase
    {
        private readonly LevelStateMachine _machine;
        private readonly BallPool _ballPool;
        private readonly Cannon[] _cannons;
        private readonly SharkContainer _sharkContainer;
        private readonly BarrelContainer _barrelContainer;
        private readonly Level _level;

        public RebornLevelState(LevelStateMachine machine)
        {
            _machine = machine;
            _ballPool = Object.FindAnyObjectByType<BallPool>();
            _sharkContainer = Object.FindAnyObjectByType<SharkContainer>();
            _cannons = Object.FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _barrelContainer = Object.FindAnyObjectByType<BarrelContainer>();
            _level = Object.FindAnyObjectByType<Level>();
        }

        public override void Enter()
        {
            _ballPool.Clear();
            _sharkContainer.Respawn();
            _barrelContainer.Respawn();
            foreach (Cannon cannon in _cannons)
                cannon.Stop();
        
            _level.Reborn();
            _machine.ChangeState<StayLevelState>();
        }

        public override void Exit()
        {
        }
    }
}