using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        private CheckPointHandler _handler;
        
        public bool IsChecked { get; set; }
        public Vector3 SpawnPosition => _spawnPoint.position;

        public void Construct(CheckPointHandler handler)
        {
            _handler = handler;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Boat") && !IsChecked)
                _handler.Check(this);
        }
    }
}