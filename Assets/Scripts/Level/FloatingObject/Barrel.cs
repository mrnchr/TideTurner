using UnityEngine;

public class Barrel : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating; 
    
    private MoonData _moon;
    public bool _inWater;
    private Rigidbody2D rb;
    
    public void Construct(MoonData moon)
    {
        _moon = moon;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
    }
    
    public void UpdateLogic()
    {
        foreach (var floatingObject in floating)
            floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInParent<Water>())
            _inWater = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInParent<Water>())
            _inWater = false;
    }
}
