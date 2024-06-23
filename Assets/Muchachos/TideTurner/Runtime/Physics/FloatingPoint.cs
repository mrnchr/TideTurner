using Muchachos.TideTurner.Runtime.Level;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Physics
{
    public class FloatingPoint : MonoBehaviour
    {
        [SerializeField]
        private FloatingBody _body;

        [Range(0, 25)]
        [SerializeField]
        private float _floatingSpeed = 1f;

        private WaterMovement _waterMovement;
        private float _velocityRate;
        
        public bool InWater { get; set; }
        
        public void Awake()
        {
            _waterMovement = FindAnyObjectByType<WaterMovement>();
        }

        public void UpdateFloatingForce()
        {
            float waterLevel =
                _waterMovement.GetMeshWaterLevel(transform.position.x + _body.Rb.velocity.x * Time.fixedDeltaTime);

            // distance between water level and bottom point of object
            float k = waterLevel - (transform.position.y - _body.Height / 2);
            // rate of floated in water body based on its height
            // alpha >= 1 - there is body full in water
            // alpha <= 0 - there is body above the water
            float alpha = Mathf.Clamp(k / _body.Height, 0, 1);

            InWater = alpha > 0;
            if (InWater)
            {
                _body.Rb.AddForceAtPosition(Vector3.up * (alpha * _floatingSpeed), transform.position,
                    ForceMode2D.Force);
            }
        }
    }
}