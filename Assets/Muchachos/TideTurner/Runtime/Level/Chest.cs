using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class Chest : MonoBehaviour
    {
        private Level _level;
        private void Awake()
        {
            _level = FindAnyObjectByType<Level>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Boat") == false)
                return;

            _level.Win();
        }
    }
}