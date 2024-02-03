using UnityEngine;

namespace DefaultNamespace.Level
{
    public class Water : MonoBehaviour, ILevelUpdatable
    {
        private Moon _moon;
        private WaterMovement _waterMovement;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
            _waterMovement = FindAnyObjectByType<WaterMovement>();
        }

        public void Init()
        {
            _waterMovement.ChangeWaterLevel(_moon.MoonSize);
        }
        
        public void UpdateLogic()
        {
            _waterMovement.ChangeWaterLevel(_moon.MoonSize);
        }
    }
}