using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core
{
    public abstract class Bootstrap : MonoBehaviour
    {
        public abstract void Construct();
        public abstract void Init();
    }
}