using System;
using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Common.Fsm;
using Muchachos.TideTurner.Runtime.Core.Input;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class LevelStateMachine : IStateMachine<LevelStateBase>, IInitializable, IDisposable
    {
        private readonly ILevelStateFactory _factory;
        private readonly List<LevelStateBase> _states = new List<LevelStateBase>();

        public LevelStateBase CurrentState { get; private set; }
        public event Action<LevelStateBase> OnChangeState;

        private readonly IInputController _inputController;

        public LevelStateMachine(ILevelStateFactory factory, IInputController inputController)
        {
            _factory = factory;
            _inputController = inputController;
            
            _inputController.OnInputHandled += HandleInput;
        }

        public void Initialize()
        {
            _states.AddRange(new LevelStateBase[]
            {
                _factory.Create<StartLevelState>(),
                _factory.Create<StayLevelState>(),
                _factory.Create<PauseLevelState>(),
                _factory.Create<RestartLevelState>(),
                _factory.Create<LoseLevelState>(),
                _factory.Create<WinLevelState>(),
                _factory.Create<RebornLevelState>()
            });
        }

        public void ChangeState<T>() where T : LevelStateBase
        {
            CurrentState?.Exit();

            CurrentState = _states.Find(x => x is T);
            CurrentState?.Enter();

            OnChangeState?.Invoke(CurrentState);
        }

        private void HandleInput(InputData data)
        {
            switch (data.IsPause)
            {
                case true when CurrentState is not PauseLevelState && 
                               CurrentState is not LoseLevelState&& 
                               CurrentState is not WinLevelState:
                    ChangeState<PauseLevelState>();
                    //Debug.Log("Pause");
                    break;
                case false when CurrentState is not StayLevelState && 
                                CurrentState is not LoseLevelState && 
                                CurrentState is not WinLevelState:
                    ChangeState<StayLevelState>();
                    //Debug.Log("Stay");
                    break;
            }
        }

        public void Dispose()
        {
            _inputController.OnInputHandled -= HandleInput;
        }
    }
}