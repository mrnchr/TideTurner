using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Shark;
using Zenject;

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

        [Inject]
        public RebornLevelState(LevelStateMachine machine,
            BallPool ballPool,
            SharkContainer sharkContainer,
            BarrelContainer barrelContainer,
            Level level)
        {
            _machine = machine;
            _ballPool = ballPool;   
            _sharkContainer = sharkContainer;
            _barrelContainer = barrelContainer;
            _level = level;
            _cannons = level.Cannons;
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