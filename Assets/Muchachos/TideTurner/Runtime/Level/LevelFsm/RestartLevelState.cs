using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Shark;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class RestartLevelState : LevelStateBase
    {
        private readonly BallPool _ballPool;
        private readonly Cannon[] _cannons;
        private readonly SharkContainer _sharkContainer;
        private readonly SoundRestarter _sound;
        private readonly BarrelContainer _barrelContainer;

        public RestartLevelState(LevelStateMachine machine) : base(machine)
        {   
            _ballPool = Object.FindAnyObjectByType<BallPool>();
            _sharkContainer = Object.FindAnyObjectByType<SharkContainer>();
            _cannons = Object.FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _sound = Object.FindAnyObjectByType<SoundRestarter>();
            _barrelContainer = Object.FindAnyObjectByType<BarrelContainer>();
        }

        public override void Enter()
        {
            _sound.SetSoundAllPlayed(SoundState.Stop);
            _ballPool.Clear();
            _sharkContainer.Respawn();
            _barrelContainer.Respawn();
            foreach (Cannon cannon in _cannons)
                cannon.Stop();
            
            _machine.ChangeState<StartLevelState>();
        }

        public override void Exit()
        {
        }
    }
}