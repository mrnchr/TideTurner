﻿namespace Muchachos.TideTurner.Runtime.Common.Fsm
{
    public interface IStateMachine<TState>
    {
        public TState CurrentState { get; }
        public void ChangeState<T>() where T : TState;
    }
}