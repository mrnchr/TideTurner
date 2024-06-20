using Muchachos.TideTurner.Runtime.Common.Fsm;

namespace Muchachos.TideTurner.Runtime.Core
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IState;
    }
}