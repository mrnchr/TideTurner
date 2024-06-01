using UnityEngine;
using UnityEngine.UI;

public class MobileMoon : AbstractMoon
{
    public Slider slider;
    
    public override void Init()
    {
        transform.localScale = Vector3.one;
    }
}