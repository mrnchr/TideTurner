using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class BarrelFactory : IBarrelFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly Barrel _prefab;
        private readonly Transform _parent;
        private readonly Level _level;

        public BarrelFactory(IInstantiator instantiator, Barrel prefab, Transform parent, Level level)
        {
            _instantiator = instantiator;
            _prefab = prefab;
            _parent = parent;
            _level = level;
        }

        public Barrel Create(Transform spawn)
        {
            Barrel instance = _instantiator.InstantiatePrefabForComponent<Barrel>(_prefab, spawn.position, Quaternion.identity, _parent);

            instance.GetComponent<Obstacle>().Construct(_level);

            return instance;
        }
    }
}