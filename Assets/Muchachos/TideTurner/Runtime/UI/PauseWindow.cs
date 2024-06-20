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
        public void Construct(LevelStateMachine levelMachine, IInputController input)
        {
            _levelMachine = levelMachine;
            _input = input;
            _input.OnInputHandled += HandleInput;
        }

        public void Construct()
        {
            _level = FindAnyObjectByType<Level.Level>();
        }

        private void OnDestroy()
        {
            _input.OnInputHandled -= HandleInput;
        }

        private void HandleInput(InputData data)
        {
            if (data.IsPause && _levelMachine.CurrentState is not WinLevelState && !_level.IsLose())
                Pause(!_isPause);
        }

        public void Pause(bool value)
        {
            if (_isPause == value)
                return;

            _isPause = value;
            if (value)
            {
                _levelMachine.ChangeState<PauseLevelState>();
            }
            else
            {
                _levelMachine.ChangeState<StayLevelState>();
            }
        
            _pauseWindow.SetActive(value);
        }
    }
}