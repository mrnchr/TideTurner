using System.Collections.Generic;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPointHandler : MonoBehaviour
    {
        private List<CheckPoint> _checks;
        private int _lastCheckIndex;
        private Level _level;

        public void Construct(CheckPoint[] checks, Level level)
        {
            _checks = new List<CheckPoint>(checks);
            _level = level;

            foreach (CheckPoint check in checks)
                check.Construct(this);
        }

        public void Init()
        {
            foreach (CheckPoint check in _checks)
                check.IsChecked = false;
            _lastCheckIndex = -1;
        }

        public void Check(CheckPoint check)
        {
            if (_level.IsLose())
                return;
        
            check.IsChecked = true;
            _lastCheckIndex++;
            _checks.Insert(_lastCheckIndex, check);
        }

        public bool WasCheckPoint()
        {
            return _lastCheckIndex > -1;
        }

        public Vector3 GetSpawnPosition()
        {
            return WasCheckPoint() ? _checks[_lastCheckIndex].SpawnPosition : Vector3.zero;
        }
    }
}