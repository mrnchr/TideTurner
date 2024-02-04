using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shark : MonoBehaviour
{
    [Range(0f,3f)][SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _initalDir;
    public void Construct()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _initalDir = transform.right;
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = _initalDir * _speed;
    }

    private void OnDisable()
    {
        _rb.velocity = _initalDir * _speed;
    }

    public void Init()
    {
        _rb.velocity = _initalDir * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        _initalDir = -_initalDir;
        _rb.velocity = _initalDir * _speed;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}