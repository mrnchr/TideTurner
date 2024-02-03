using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{
    [Range(1,10f)][SerializeField] private float displacementAmount = 1;
    [Range(0.1f,2f)][SerializeField] private float waterDrag = 1f;
    [Range(0.1f,2f)][SerializeField] private float waterAngularDrag = 1f;
    [Range(0.1f,5f)][SerializeField] private float floaterCount = 1;
    [Range(0.1f,5f)][SerializeField] private float depthBefore = 1;
    [SerializeField] private float velocityMultiplier;
    
    private Rigidbody _rb;
    private WaterMovement _waterMovement;
    private float _displacementMultiplayer;
    private float _waveHeight;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _waterMovement = FindObjectOfType<WaterMovement>();

        _rb.drag = 1;
        _rb.angularDrag = 1;

        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationY;
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        Move();
    }

    public void SetVelocityRate(float velocity)
    {
        var vel = _rb.velocity;
        vel.x = velocity * velocityMultiplier;
        _rb.velocity = vel;
    }

    private void Move()
    {
        _rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // get current sin value;
        _waveHeight = _waterMovement.GetWaveHeight(transform.position.x); //+ _waterMovement.GetWaterLevel().position.y;
        
        if ( transform.position.y - _waterMovement.GetWaterLevel().position.y >  _waveHeight) 
            return;
        
        _displacementMultiplayer = Mathf.Clamp01( (_waveHeight - transform.position.y) / depthBefore ) * displacementAmount;
            
        // vector up * displace;
        Vector3 v = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * _displacementMultiplayer, 0f);
            
        _rb.AddForceAtPosition(v, transform.position, ForceMode.Acceleration);
            
        _rb.AddForce(_displacementMultiplayer * -_rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
        _rb.AddTorque(_displacementMultiplayer * -_rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
}
