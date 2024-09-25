using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPointHandler : MonoBehaviour
    {
        private const int DefaultCheckIndex = -1;

        [SerializeField] private List<CheckPoint> _checks;

        public event Action<int> OnNewCheckPoint;

        private int _lastCheckIndex = DefaultCheckIndex;
        private Level _level;
        private User _user;

        [Inject]
        public void Construct(Level level, User user)
        {
            _level = level;
            _user = user;

            UpdateCheckPointIndex();

            Init();
        }

        private void Init()
        {
            int i = 0;
            foreach (CheckPoint check in _checks)
            {
                check.IsChecked = i < _user.Data.CurrentInd;

                check.index = i++;
            }

            _lastCheckIndex = _user.Data.CurrentInd;
        }

        public void Check(CheckPoint check)
        {
            if (_level.IsLose())
                return;

            check.IsChecked = true;
            _lastCheckIndex = check.index;
            _checks.Insert(_lastCheckIndex, check);

            OnNewCheckPoint?.Invoke(_lastCheckIndex);
        }

        public void UpdateCheckPointIndex() => _lastCheckIndex = _user.Data.CurrentInd;
        public bool WasCheckPoint() => _lastCheckIndex > DefaultCheckIndex;
        public Vector3 GetSpawnPosition() => WasCheckPoint() ? _checks[_lastCheckIndex].SpawnPosition : Vector3.zero;
    }
}