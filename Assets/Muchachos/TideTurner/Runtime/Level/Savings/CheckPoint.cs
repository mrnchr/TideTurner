using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Savings
{
    public class CheckPoint : MonoBehaviour
    {
        public bool IsChecked { get; set; }
        private CheckPointHandler _handler;

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