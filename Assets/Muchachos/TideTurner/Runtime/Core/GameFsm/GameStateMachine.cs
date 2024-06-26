using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Common.Fsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public class GameStateMachine : IStateMachine<GameStateBase>, IInitializable
    {
        private readonly IGameStateFactory _factory;
        private readonly List<GameStateBase> _states = new List<GameStateBase>();

        public GameStateBase CurrentState { get; private set; }

        public GameStateMachine(IGameStateFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _states.AddRange(new GameStateBase[]
            {
                _factory.Create<MenuGameState>(),
                _factory.Create<LevelGameState>()
            });
        }

        public void ChangeState<T>() where T : GameStateBase
        {
            CurrentState?.Exit();

            CurrentState = _states.Find(x => x is T);
            CurrentState?.Enter();
        }
    }
}