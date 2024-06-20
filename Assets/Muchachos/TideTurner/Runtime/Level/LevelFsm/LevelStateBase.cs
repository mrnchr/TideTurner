using Muchachos.TideTurner.Runtime.Common.Fsm;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public abstract class LevelStateBase : IState
    {
        public abstract void Enter();
        public abstract void Exit();
    }
}