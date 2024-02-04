using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [Header("Settings")]
    [Range(1f,10f)][SerializeField] private float velocityMultiplier = 4;
    
    private WaterMovement _waterMovement;
    private Vector3 dir;
    private void Awake()
    {
        _waterMovement = FindObjectOfType<WaterMovement>();
    }

    private void Update()
    {
        Move();
    }

    public void SetVelocityRate(float velocity)
    {
        rb.velocity = new Vector3(velocity * velocityMultiplier, rb.velocity.y, rb.velocity.z);
    }

    private void Move()
    {
        dir = new Vector3(
                          0, 
                          _waterMovement.GetWaterLevel().position.y + _waterMovement.GetWaveHeight(transform.position.x),
                          0f) - new Vector3(0, transform.position.y, 0f);
        rb.AddForce(dir, ForceMode.Force);
    }
}

//var vel = rb.velocity;
//vel.x = velocity * velocityMultiplier;
//rb.velocity = vel;    