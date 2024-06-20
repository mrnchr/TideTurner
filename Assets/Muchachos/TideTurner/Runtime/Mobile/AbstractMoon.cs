using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public abstract class AbstractMoon : MonoBehaviour, ILevelUpdatable
    {
        public abstract void Init();
    }
}
