using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [Header("Settings")]
    [Range(1,10f)][SerializeField] private float displacementAmount = 1;
    [Range(0.1f,2f)][SerializeField] private float waterDrag = 2f;
    [Range(0.1f,2f)][SerializeField] private float waterAngularDrag = 2f;
    [Range(0.1f,5f)][SerializeField] private float floaterCount = 2;
    [Range(0.1f,5f)][SerializeField] private float depthBefore = 1;
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
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // get current sin value;
        _waveHeight = _waterMovement.GetWaveHeight(transform.position.x);
        
        if ( transform.position.y - _waterMovement.GetWaterLevel().position.y >  _waveHeight) 
            return;
        
        _displacementMultiplayer = Mathf.Clamp01( (_waveHeight - transform.position.y) / depthBefore ) * displacementAmount;
            
        // vector up * displace;
        Vector3 v = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * _displacementMultiplayer, 0f);
            
        rb.AddForceAtPosition(v, transform.position, ForceMode.Acceleration);
            
        rb.AddForce(_displacementMultiplayer * -rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(_displacementMultiplayer * -rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
}