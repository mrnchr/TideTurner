using System;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public event Action OnPlayerCollision;

        private Level _level;

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TagStorage.BoatTag))
                OnCollided();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagStorage.BoatTag))
                OnCollided();
        }

        private void OnCollided()
        {
            OnPlayerCollision?.Invoke();
            _level.Lose();
        }
    }
}