using UnityEngine;

public class Barrel : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    
    private MoonData _moon;
    public void Construct(MoonData moon)
    {
        _moon = moon;
    }
    
    public void UpdateLogic()
    {
        foreach (var floatingObject in floating)
        {
            floatingObject.SetVelocityRate(_moon.MoonPosition);
        }
    }
}
