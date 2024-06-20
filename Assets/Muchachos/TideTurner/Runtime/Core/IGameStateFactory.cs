using Muchachos.TideTurner.Runtime.Core.GameFsm;

namespace Muchachos.TideTurner.Runtime.Core
{
    public interface IGameStateFactory
    {
        TState Create<TState>() where TState : GameStateBase;
    }
}