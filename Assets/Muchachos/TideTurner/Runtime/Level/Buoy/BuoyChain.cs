using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Buoy
{
    public class BuoyChain : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _line;

        [SerializeField]
        private Transform _bracing;

        private void Start()
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, _bracing.position);
        }

        private void Update()
        {
            _line.SetPosition(1, _bracing.position);
        }
    }
}