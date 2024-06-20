using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Common.Fsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class LevelStateMachine : IStateMachine<LevelStateBase>, IInitializable
    {
        private readonly ILevelStateFactory _factory;
        private readonly List<LevelStateBase> _states = new List<LevelStateBase>();

        public LevelStateBase CurrentState { get; private set; }

        public LevelStateMachine(ILevelStateFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _states.AddRange(new LevelStateBase[]
            {
                _factory.Create<StartLevelState>(),
                _factory.Create<StayLevelState>(),
                _factory.Create<PauseLevelState>(),
                _factory.Create<RestartLevelState>(),
                _factory.Create<LoseLevelState>(),
                _factory.Create<WinLevelState>(),
                _factory.Create<RebornLevelState>()
            });
        }

        public void ChangeState<T>() where T : LevelStateBase
        {
            CurrentState?.Exit();

            CurrentState = _states.Find(x => x is T);
            CurrentState?.Enter();
        }
    }
}