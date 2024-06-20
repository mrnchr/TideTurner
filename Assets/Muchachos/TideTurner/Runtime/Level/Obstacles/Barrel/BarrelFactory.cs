using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class BarrelFactory
    {
        private readonly Barrel _prefab;
        private readonly Transform _parent;
        private readonly AbstractMoonData _moonData;
        private readonly LevelUpdater _updater;
        private readonly WaterMovement _waterMovement;
        private readonly global::Muchachos.TideTurner.Runtime.Level.Level _level;

        public BarrelFactory(
            Barrel prefab,
            Transform parent,
            AbstractMoonData moonData,
            LevelUpdater updater,
            WaterMovement waterMovement,
            global::Muchachos.TideTurner.Runtime.Level.Level level)
        {
            _prefab = prefab;
            _parent = parent;
            _moonData = moonData;
            _updater = updater;
            _waterMovement = waterMovement;
            _level = level;
        }

        public Barrel Create(Transform spawn)
        {
            Barrel instance = Object.Instantiate(_prefab, spawn.position, Quaternion.identity, _parent);
            instance.Construct(_moonData, _waterMovement);

            var floating = instance.GetComponentInChildren<FloatingObject>();
            floating.Construct(_waterMovement);

            instance.GetComponent<Obstacle>().Construct(_level);

            _updater.Add(instance);
            _updater.Add(floating);
            _updater.Add(floating.GetComponent<FloatingConstant>());

            return instance;
        }
    }
}