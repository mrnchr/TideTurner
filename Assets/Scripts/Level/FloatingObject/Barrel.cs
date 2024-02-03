using UnityEngine;

public class Barrel : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    
    private Moon _moon;
    public void Construct()
    {
        _moon = FindAnyObjectByType<Moon>();
    }
    
    public void UpdateLogic()
    {
        foreach (var floatingObject in floating)
        {
            floatingObject.SetVelocityRate(_moon.MoonPosition);
        }
    }
}
