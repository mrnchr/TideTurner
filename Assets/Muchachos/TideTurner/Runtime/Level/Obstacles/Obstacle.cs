using System;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public event Action OnPlayerCollision;

        private Level _level;

        public void Construct(Level level)
        {
            _level = level;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Boat"))
                OnCollided();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Boat"))
                OnCollided();
        }

        private void OnCollided()
        {
            OnPlayerCollision?.Invoke();
            _level.Lose();
        }
    }
}