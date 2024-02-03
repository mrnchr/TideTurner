using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{
    private float waveHeight = 3;
    [Range(1,10f)][SerializeField] private float displacementAmount = 1;
    [Range(1,2f)][SerializeField] private float waterDrag = 1f;
    [Range(1,2f)][SerializeField] private float waterAngularDrag = 1f;
    [Range(1,5f)][SerializeField] private float floaterCount = 1;
    [Range(1,5f)][SerializeField] private float depthBefore = 1;
    [SerializeField] private float velocityMultiplier;
    
    private Rigidbody _rb;
    private Water _water;
    private float _displacementMultiplayer;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _water = FindObjectOfType<Water>();

        _rb.drag = 1;
        _rb.angularDrag = 1;

        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationY;
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        Move();

        //AddForce(new Vector2(-1, -1));
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

        waveHeight = _water.GetWaveHeight(transform.position.x) + _water.GetWaterLevel().position.y;
        
        if (transform.position.y >  waveHeight) 
            return;
        
        _displacementMultiplayer = Mathf.Clamp01( (waveHeight - transform.position.y) / depthBefore ) * displacementAmount;
            
        Vector3 v = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * _displacementMultiplayer, 0f);
            
        _rb.AddForceAtPosition(v, transform.position, ForceMode.Acceleration);
            
        _rb.AddForce(_displacementMultiplayer * -_rb.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
        _rb.AddTorque(_displacementMultiplayer * -_rb.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
}
