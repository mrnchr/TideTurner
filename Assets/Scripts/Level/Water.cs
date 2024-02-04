using UnityEngine;

public class Water : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    private MoonData _moon;
    private WaterMovement _waterMovement;

    public WaterMovement Movement => _waterMovement;

    public void Construct(MoonData moon)
    {
        _moon = moon;
        _waterMovement = FindAnyObjectByType<WaterMovement>();
    }

    public void Init()
    {
        _waterMovement.Init();
        _waterMovement.ChangeWaterLevel(_moon.MoonSize);
    }
        
    public void UpdateLogic()
    {
        _waterMovement.ChangeWaterLevel(_moon.MoonSize);
    }
}