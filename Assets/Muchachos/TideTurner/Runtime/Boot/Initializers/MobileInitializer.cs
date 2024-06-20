using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot.Initializers
{
    public class MobileInitializer : IInitializable
    {
        public void Initialize()
        {
            UnityEngine.Input.gyro.enabled = true;
        }
    }
}