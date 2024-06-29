using Muchachos.TideTurner.Runtime.Level.Savings;
using Muchachos.TideTurner.Runtime.Physics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Muchachos.TideTurner.Runtime.Level.Buoy
{
    public class BuoySwitcher : MonoBehaviour
    {
        private static readonly int _float = Animator.StringToHash("Float");

        [SerializeField]
        private Light2D _light;

        [SerializeField]
        private FloatingBody _body;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Collider2D _idleCollider;

        [SerializeField]
        private Collider2D _floatCollider;

        [SerializeField]
        private CheckPoint _checkPoints;

        private void Start()
        {
            _light.enabled = false;
            _body.enabled = false;
            _idleCollider.enabled = true;
            _floatCollider.enabled = false;
        }

        private void Update()
        {
            if (_checkPoints.IsChecked)
            {
                _light.enabled = true;
                _body.enabled = true;
                _idleCollider.enabled = false;
                _floatCollider.enabled = true;
                _animator.SetBool(_float, true);
            }
        }
    }
}