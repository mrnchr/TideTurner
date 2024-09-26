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
            
            _user.OnDataUpdate += UpdateCheckPointIndex;
            OnNewCheckPoint += WrappedUpdateData;
        }

        public void Init()
        {
            int i = 0;

            string checkedPoints = "Checked points: ";
            
            foreach (CheckPoint check in _checks)
            {
                bool isCheched = i <= _user.Data.CurrentInd;
                check.Init(isCheched, i++);
                
                checkedPoints += isCheched ? i + " " : string.Empty;
            }
            
            Debug.Log(checkedPoints);

            UpdateCheckPointIndex();
        }

        public void Check(CheckPoint check)
        {
            if (_level.IsLose())
                return;

            check.IsChecked = true;
            _lastCheckIndex = check.Index;
            //_checks.Insert(_lastCheckIndex, check);

            OnNewCheckPoint?.Invoke(_lastCheckIndex);
        }
        
        private void WrappedUpdateData(int ind) => 
            _user.UpdateData(new UserData(_user.Data.Nick, ind, _user.Data.Authorised)); 

        private void UpdateCheckPointIndex() => _lastCheckIndex = _user.Data.CurrentInd;
        public bool WasCheckPoint() => _lastCheckIndex > DefaultCheckIndex;
        public Vector3 GetSpawnPosition() => WasCheckPoint() ? _checks[_lastCheckIndex].SpawnPosition : Vector3.zero;

        private void OnDestroy()
        {
            _user.OnDataUpdate -= UpdateCheckPointIndex;
            OnNewCheckPoint -= WrappedUpdateData;
        }
    }
}