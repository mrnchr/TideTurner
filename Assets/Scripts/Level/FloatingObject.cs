using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [Header("Settings")]
    [Range(1f,10f)][SerializeField] private float velocityMultiplier = 4;
    
    private WaterMovement _waterMovement;
    private float _displacementMultiplayer;
    private float _waveHeight;
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
        var vel = rb.velocity;
        vel.x = velocity * velocityMultiplier;
        rb.velocity = vel;
    }

    private void Move()
    {
        Vector3 dir = new Vector3(
                          0, 
                          _waterMovement.GetWaterLevel().position.y + _waterMovement.GetWaveHeight(transform.position.x),
                          0f) - new Vector3(0, transform.position.y, 0f);
        rb.AddForce(dir, ForceMode.Force);
    }
}

/*
    [Range(0,10f)][SerializeField] private float displacementAmount = 1;
    [Range(0.1f,2f)][SerializeField] private float waterDrag = 2f;
    [Range(0.1f,2f)][SerializeField] private float waterAngularDrag = 2f;
    [Range(0.1f,5f)][SerializeField] private float floaterCount = 2;
    [Range(0.1f,5f)][SerializeField] private float depthBefore = 1;
    */
/*
private void Move()
    {
        // go down.
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // get current sin value;
        _waveHeight = _waterMovement.GetWaveHeight(transform.position.x);
        
         if(   _waterMovement.GetWaterLevel().position.y < transform.position.y) 
            return;
        
        _displacementMultiplayer = Mathf.Clamp01( (_waveHeight - transform.position.y ) / depthBefore ) * displacementAmount;
            
        // Go up.
        Vector3 v = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * _displacementMultiplayer, 0f);
            
        rb.AddForceAtPosition(v, transform.position, ForceMode.Acceleration);
            
        rb.AddForce(_displacementMultiplayer * -rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(_displacementMultiplayer * -rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
    */