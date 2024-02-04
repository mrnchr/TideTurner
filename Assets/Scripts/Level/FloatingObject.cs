using UnityEngine;

public class FloatingObject : MonoBehaviour, ILevelUpdatable, IFixedUpdatable
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [Range(5f, 15f)] [SerializeField] private float horizontalVelocityMultiplier = 10;

    [Range(1f, 25f)] [SerializeField] private float verticalVelocityMultiplier = 1f;


    private WaterMovement _waterMovement;
    private Vector3 dir;
    private float _velocityRate;

    public void Construct(WaterMovement waterMovement)
    {
        _waterMovement = waterMovement;
    }

    public void FixedUpdateLogic()
    {
        Move();
        AddHorizontalVelocity();
        ClampHorizontalVelocity();
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
        dir = new Vector3(
            0,
            _waterMovement.GetWaterLevel().position.y + _waterMovement.GetWaveHeight(transform.position.x),
            0f) - new Vector3(0, transform.position.y, 0f);

        rb.AddForceAtPosition(dir * verticalVelocityMultiplier, transform.position, ForceMode2D.Force);
    }
}