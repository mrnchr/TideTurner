using System;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        public int Index { get; private set; }
        public bool IsChecked { get; set; }
        public event Action OnChecked;
        public Vector3 SpawnPosition => _spawnPoint.position;

        private CheckPointHandler _handler;

        [Inject]
        public void Construct(CheckPointHandler handler)
        {
            _handler = handler;
        }

        public void Init(bool isChecked, int ind)
        {
            IsChecked = isChecked;
            Index = ind;
            
            if (isChecked)
                OnChecked?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool isBoat = other.CompareTag(TagStorage.BoatTag);
            if (!isBoat || IsChecked)
                return;

            _handler.Check(this);
            OnChecked?.Invoke();
            Debug.Log(other.name);
        }
    }
}