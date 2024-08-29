using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        public bool IsChecked { get; set; }
        public Vector3 SpawnPosition => _spawnPoint.position;

        private CheckPointHandler _handler;

        [Inject]
        public void Construct(CheckPointHandler handler)
        {
            _handler = handler;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagStorage.BoatTag) && !IsChecked)
                _handler.Check(this);
        }
    }
}