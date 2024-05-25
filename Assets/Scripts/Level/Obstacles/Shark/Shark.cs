using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Shark : MonoBehaviour
{
    [Range(0f,3f)][SerializeField] private float _speed;

    [SerializeField] private GameObject[] shadows;

    private Rigidbody2D _rb;
    private BoxCollider2D _box;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _initialDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
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
        _box.enabled = true;
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
        shadows[0].SetActive(_initialDir.x > 0);
        shadows[1].SetActive(_initialDir.x <= 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            _rb.velocity = Vector3.zero;
            _box.enabled = false;
            return;
        }

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        SetDirection(-_initialDir);
    }
}