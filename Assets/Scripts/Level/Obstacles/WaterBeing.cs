using UnityEngine;

public class WaterBeing : MonoBehaviour, ILevelUpdatable
{
    private Water _water;

    public void Construct(Water water)
    {
        _water = water;
    }
    
    public void UpdateLogic()
    {
        gameObject.SetActive(_water.Movement.GetWaterLevel().position.y > transform.position.y);
    }
}