using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Physics
{
    public class FloatingPoint : MonoBehaviour
    {
        [SerializeField]
        private FloatingBody _body;

        [Range(0, 25)]
        [SerializeField]
        private float _floatingSpeed = 1f;

        public bool InWater { get; set; }
        
        public void UpdateFloatingForce()
        {
            float depthRate = _body.GetVolumeRate(transform.position);

            InWater = depthRate > 0;
            if (InWater)
            {
                _body.Rb.AddForceAtPosition(Vector3.up * (depthRate * _floatingSpeed), transform.position,
                    ForceMode2D.Force);
            }
        }
    }
}