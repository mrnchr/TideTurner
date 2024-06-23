using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.FloatingObjects
{
    public class Barrel : MonoBehaviour, ILevelUpdatable, IUpdatable
    {
        [SerializeField] private float _maxVelocity;
        [SerializeField] private FloatingObject[] floating;
        [SerializeField] private SoundPlayer boomSound;
        [SerializeField] private ParticleSystem boomEffect;

        private bool _inWater;
        private AbstractMoonData _moon;
        private Obstacle _obstacle;
        private Rigidbody2D rb;
        private WaterMovement _waterMovement;

        public void Construct(AbstractMoonData moon, WaterMovement waterMovement)
        {
            _obstacle = GetComponent<Obstacle>();
            rb = GetComponent<Rigidbody2D>();
            _moon = moon;
            _waterMovement = waterMovement;

            _obstacle.OnPlayerCollision += SubsribeToObstacle;
        }

        public void Init()
        {
            gameObject.SetActive(true);
            rb.rotation = 0;
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
            transform.eulerAngles = new Vector3(0, 0, 0);
            boomEffect.transform.localPosition = Vector3.zero;
        }

        private void SubsribeToObstacle()
        {
            boomSound.SetSoundState(SoundState.Play);
            boomEffect.transform.parent = null;
            boomEffect.Play();
            gameObject.SetActive(false);
        }

        public void UpdateLogic()
        {
            CheckInWater();
        
            foreach (var floatingObject in floating)
                floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);

            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -_maxVelocity, _maxVelocity), Mathf.Clamp(rb.velocity.y, -_maxVelocity, _maxVelocity));
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -_maxVelocity, _maxVelocity);
            //TestRot();
        }

        private void CheckInWater()
        {
            _inWater = _waterMovement.GetWaterLevel().position.y > transform.position.y;
        }

        private void TestRot()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                rb.AddTorque(500);
                rb.AddForce(-Vector3.one, ForceMode2D.Impulse);
                //Debug.Log("added");
            }
        }
    }
}
