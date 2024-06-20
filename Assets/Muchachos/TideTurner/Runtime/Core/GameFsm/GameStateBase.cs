using Muchachos.TideTurner.Runtime.Common.Fsm;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public abstract class GameStateBase : IState
    {
        protected GameStateMachine _machine;

        protected GameStateBase(GameStateMachine machine)
        {
            _machine = machine;
        }

        public abstract void Enter();
        public abstract void Exit();
    }
}