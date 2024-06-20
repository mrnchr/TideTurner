using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseWindow;

        private InputController _input;
        private LevelStateMachine _machine;
        private bool _isPause;
        private Level.Level _level;

        public void Construct()
        {
            _level = FindAnyObjectByType<Level.Level>();
            _input = FindAnyObjectByType<InputController>();
            _machine = FindAnyObjectByType<LevelStateMachine>();
            _input.OnInputHandled += HandleInput;
        }

        private void OnDestroy()
        {
            _input.OnInputHandled -= HandleInput;
        }

        private void HandleInput(InputData data)
        {
            if (data.IsPause && _machine.CurrentState is not WinLevelState && !_level.IsLose())
                Pause(!_isPause);
        }

        public void Pause(bool value)
        {
            if (_isPause == value)
                return;

            _isPause = value;
            if (value)
            {
                _machine.ChangeState<PauseLevelState>();
            }
            else
            {
                _machine.ChangeState<StayLevelState>();
            }
        
            _pauseWindow.SetActive(value);
        }
    }
}