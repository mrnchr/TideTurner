using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shark : MonoBehaviour
{
    [Range(0f,3f)][SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _initialDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Construct()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _initialDir = transform.right;
    }

    private void OnEnable()
    {
        _rb.velocity = _initialDir * _speed;
    }
    
    public void Init()
    {
        SetVelocity();
    }

    public void SetDirection(Vector3 direction)
    {
        _initialDir = direction;
        SetVelocity();
    }

    private void SetVelocity()
    {
        _rb.velocity = _initialDir * _speed;
        _spriteRenderer.flipX = _initialDir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        SetDirection(-_initialDir);
    }
}