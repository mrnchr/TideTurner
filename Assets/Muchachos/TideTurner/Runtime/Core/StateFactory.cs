using Muchachos.TideTurner.Runtime.Common.Fsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Core
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState Create<TState>() where TState : IState
        {
            return _instantiator.Instantiate<TState>();
        }
    }
}