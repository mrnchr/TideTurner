using Muchachos.TideTurner.Runtime.Core.GameFsm;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core
{
    public class ProjectContext : MonoBehaviour
    {
        private bool _isInitialized;
        private GameStateMachine _machine;

        public bool IsInitialized => _isInitialized;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _machine = GetComponentInChildren<GameStateMachine>();
        
            _machine.Construct();
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (_isInitialized)
                return;
        
            _isInitialized = true;
        }
    }
}