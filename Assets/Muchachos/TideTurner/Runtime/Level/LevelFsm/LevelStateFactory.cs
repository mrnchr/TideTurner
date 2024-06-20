using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class LevelStateFactory : ILevelStateFactory
    {
        private readonly IInstantiator _instantiator;

        public LevelStateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState Create<TState>() where TState : LevelStateBase
        {
            return _instantiator.Instantiate<TState>();
        }
    }
}