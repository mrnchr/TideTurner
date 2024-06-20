using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Common.Fsm;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public class GameStateMachine : MonoBehaviour, IStateMachine<GameStateBase>
    {
        private readonly List<GameStateBase> _states = new List<GameStateBase>();

        public GameStateBase CurrentState { get; private set; }

        public void Construct()
        {
            _states.AddRange(new GameStateBase[]
            {
                new MenuGameState(this),
                new LevelGameState(this)
            });
        }

        public void ChangeState<T>() where T : GameStateBase
        {
            CurrentState?.Exit();

            CurrentState = _states.Find(x => x is T);
            CurrentState?.Enter();
        }
    }
}