using System.Collections;
using UnityEngine;

public class Tentacle : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    private static readonly int _isShownVar = Animator.StringToHash("IsShown");
    private static readonly int _attack = Animator.StringToHash("Attack");

    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Collider2D _collider;
    private Water _water;
    private bool _isShown;
    private bool _inWater;

    public void Construct(Water water)
    {
        _water = water;
    }

    public void UpdateLogic()
    {
        _inWater = _water.Movement.GetWaterLevel().position.y > transform.position.y;
        CheckForShown();
    }

    private void CheckForShown()
    {
        if (_isShown != _inWater)
        {
            _isShown = _inWater;
            Show(_isShown);
        }
    }

    private void Show(bool value)
    {
        _collider.enabled = value;
        _animator.SetBool(_isShownVar, _isShown);

        if (value)
            StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_attackDelay);
        while (_isShown)
        {
            _animator.SetTrigger(_attack);
            yield return new WaitForSeconds(_attackDelay);
        }
    }
}