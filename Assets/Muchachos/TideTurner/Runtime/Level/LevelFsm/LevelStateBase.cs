using Muchachos.TideTurner.Runtime.Common.Fsm;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public abstract class LevelStateBase : IState
    {
        protected LevelStateMachine _machine;

        protected LevelStateBase(LevelStateMachine machine)
        {
            _machine = machine;
        }
        
        public abstract void Enter();
        public abstract void Exit();
    }
}