using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BallPool _pool;

    public void Construct()
    {
        _rb = GetComponent<Rigidbody2D>();
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