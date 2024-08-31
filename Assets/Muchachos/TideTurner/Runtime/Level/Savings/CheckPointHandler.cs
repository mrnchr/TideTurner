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
        private UserData _userData;

        [Inject]
        public void Construct(Level level, UserData userData)
        {
            _level = level;
            _userData = userData;

            UpdateCheckPointIndex();
        }

        public void Init()
        {
            foreach (CheckPoint check in _checks)
                check.IsChecked = false;
        }

        public void Check(CheckPoint check)
        {
            if (_level.IsLose())
                return;

            check.IsChecked = true;
            _lastCheckIndex++;
            _checks.Insert(_lastCheckIndex, check);

            OnNewCheckPoint?.Invoke(_lastCheckIndex);
        }

        public bool WasCheckPoint()
        {
            return _lastCheckIndex > DefaultCheckIndex;
        }

        public void UpdateCheckPointIndex()
        {
            _lastCheckIndex = _userData.CurrentInd;
        }

        public Vector3 GetSpawnPosition()
        {
            return WasCheckPoint() ? _checks[_lastCheckIndex].SpawnPosition : Vector3.zero;
        }
    }
}