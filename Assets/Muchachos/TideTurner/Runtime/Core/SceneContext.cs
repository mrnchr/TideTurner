using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core
{
    public class SceneContext : MonoBehaviour
    {
        private Bootstrap _bootstrap;
        
        private void Awake()
        {
            _bootstrap = FindAnyObjectByType<Bootstrap>();
            if (_bootstrap)
                _bootstrap.Construct();
        }

        private void Start()
        {
            if(_bootstrap)
                _bootstrap.Init();
        }
    }
}