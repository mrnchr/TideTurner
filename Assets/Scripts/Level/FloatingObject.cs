using UnityEngine;

public class FloatingObject : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [Range(5f, 15f)] [SerializeField] private float horizontalVelocityMultiplier = 10;
    [Range(1f, 25f)][SerializeField] private float verticalVelocityMultiplier = 1f;


    private WaterMovement _waterMovement;
    private Vector3 dir;
    public void Construct(WaterMovement waterMovement)
    {
        _waterMovement = waterMovement;
    }

    public void UpdateLogic()
    {
        Move();
    }

    public void SetVelocityRate(float velocity)
    {
        rb.velocity = new Vector3(velocity * horizontalVelocityMultiplier, rb.velocity.y);
    }

    private void Move()
    {
        dir = new Vector3(
            0,
            _waterMovement.GetWaterLevel().position.y + _waterMovement.GetWaveHeight(transform.position.x),
            0f) - new Vector3(0, transform.position.y, 0f);

        rb.AddForceAtPosition(dir * verticalVelocityMultiplier, transform.position,ForceMode2D.Force);
    }
}