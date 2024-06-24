using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.FloatingObjects
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private SoundPlayer boomSound;
        [SerializeField] private ParticleSystem boomEffect;

        private Obstacle _obstacle;
        private Rigidbody2D _rb;

        public void Awake()
        {
            _obstacle = GetComponent<Obstacle>();
            _rb = GetComponent<Rigidbody2D>();

            _obstacle.OnPlayerCollision += SubscribeToObstacle;
        }

        public void Init()
        {
            gameObject.SetActive(true);
            _rb.rotation = 0;
            _rb.angularVelocity = 0;
            _rb.velocity = Vector2.zero;
            transform.eulerAngles = new Vector3(0, 0, 0);
            boomEffect.transform.SetParent(transform);
            boomEffect.transform.localPosition = Vector3.zero;
        }

        private void SubscribeToObstacle()
        {
            boomSound.SetSoundState(SoundState.Play);
            boomEffect.transform.parent = null;
            boomEffect.Play();
            gameObject.SetActive(false);
        }
    }
}
