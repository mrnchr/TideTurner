using Muchachos.TideTurner.Runtime.Common.Fsm;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public abstract class GameStateBase : IState
    {
        public abstract void Enter();
        public abstract void Exit();
    }
}