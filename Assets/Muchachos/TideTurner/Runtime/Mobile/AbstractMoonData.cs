using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public abstract class AbstractMoonData : MonoBehaviour
    {
        public float MoonSize;
        public float MoonPosition;
    
        public abstract void Init();
        public abstract void Construct();
    }
}