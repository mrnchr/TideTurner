using UnityEngine;

public class FloatingObject : MonoBehaviour, ILevelUpdatable, IFixedUpdatable
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [Range(1f, 15f)] [SerializeField] private float horizontalVelocityMultiplier = 10;

    [Range(1f, 25f)] [SerializeField] private float verticalVelocityMultiplier = 1f;
    
    private WaterMovement _waterMovement;
    private Vector3 depth;
    private float _velocityRate;

    public void Construct(WaterMovement waterMovement)
    {
        _waterMovement = waterMovement;
    }

    public void FixedUpdateLogic()
    {
        AddHorizontalVelocity();
        ClampHorizontalVelocity();
        Move();
    }

    private void ClampHorizontalVelocity()
    {
        var x = Mathf.Clamp(rb.velocity.x, -horizontalVelocityMultiplier, horizontalVelocityMultiplier);
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    public void SetVelocityRate(float velocity)
    {
        _velocityRate = velocity;
    }

    private void AddHorizontalVelocity()
    {
        rb.AddForce(Vector2.right * (_velocityRate * horizontalVelocityMultiplier), ForceMode2D.Force);
    }

    private void Move()
    {
        var up = _waterMovement.GetMeshWaterLevel(transform.position.x + rb.velocity.x * Time.fixedDeltaTime) - transform.position.y;
        depth = new Vector3(0, up, 0);

        if (depth.y < 0)
            return;

        rb.AddForceAtPosition(depth * verticalVelocityMultiplier, transform.position, ForceMode2D.Force);
    }
}