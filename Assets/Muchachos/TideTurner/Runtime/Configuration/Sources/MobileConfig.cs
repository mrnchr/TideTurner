using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [CreateAssetMenu(menuName = CAC.MOBILE_MENU, fileName = CAC.MOBILE_NAME)]
    public class MobileConfig : ScriptableObject
    {
        [Range(1, 10)] public float MovementSmoothness = 3f;
    }
}