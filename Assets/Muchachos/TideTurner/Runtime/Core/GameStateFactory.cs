using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly IInstantiator _instantiator;

        public GameStateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState Create<TState>() where TState : GameStateBase
        {
            return _instantiator.Instantiate<TState>();
        }
    }
}