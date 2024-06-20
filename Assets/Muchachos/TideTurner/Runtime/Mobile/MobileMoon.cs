using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMoon : AbstractMoon
    {
        public Slider slider;
    
        public override void Init()
        {
            transform.localScale = Vector3.one;
        }
    }
}