using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private BallPool _pool;

    public void Construct()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetPool(BallPool pool)
    {
        _pool = pool;
    }
    
    public void SetVelocity(Vector3 value)
    {
        _rb.velocity = value;
    }

    private void OnCollisionEnter(Collision other)
    {
        _pool.Push(this);
    }
}