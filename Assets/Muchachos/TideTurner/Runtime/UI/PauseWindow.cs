using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseWindow;

        private IInputController _input;
        private LevelStateMachine _levelMachine;
        private bool _isPause;
        private Level.Level _level;

        [Inject]
        public void Construct(LevelStateMachine levelMachine, 
            Level.Level level, 
            IInputController input)
        {
            _levelMachine = levelMachine;
            _input = input;
            _level = level;

            _input.OnInputHandled += HandleInput;
        }

        private void OnDestroy()
        {
            _input.OnInputHandled -= HandleInput;
        }

        private void HandleInput(InputData data)
        {
            if (_levelMachine.CurrentState is WinLevelState 
                || _level.IsLose()
                || data.IsPause == _pauseWindow.activeSelf)
                return;
            
            Debug.Log(_levelMachine.CurrentState);

            _pauseWindow.SetActive(data.IsPause);
        }

        public void Pause(bool value)
        {
            _input.Data.IsPause = value;
            _pauseWindow.SetActive(value);
        }
    }
}