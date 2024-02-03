using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shark : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;

    public void Construct()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        _rb.velocity = transform.right * _speed;
        SetScaleX(Mathf.Sign(_rb.velocity.x));
    }

    private void OnCollisionEnter(Collision other)
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        _rb.velocity = -transform.right * _speed;
        ChangeScale();
    }

    private void ChangeScale()
    {
        SetScaleX(-transform.localScale.x);
    }

    public void SetScaleX(float value)
    {
        Vector3 scale = transform.localScale;
        scale.x = value;
        transform.localScale = scale;
    }
}