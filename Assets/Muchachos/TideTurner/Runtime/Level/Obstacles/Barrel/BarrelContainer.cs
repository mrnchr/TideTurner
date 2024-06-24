using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class BarrelContainer : MonoBehaviour
    {
        private readonly List<Barrel> _barrels = new List<Barrel>();
    
        [SerializeField] private Barrel _prefab;
        [SerializeField] private Transform _parent;
    
        private IBarrelFactory _factory;
        private BarrelSpawn[] _spawns;

        [Inject]
        public void Construct(IBarrelFactory factory)
        {
            _factory = factory;
        }

        public void Construct(BarrelSpawn[] spawns)
        {
            _spawns = spawns;
        }

        public void Init()
        {
            foreach (BarrelSpawn spawn in _spawns)
            {
                var instance = _factory.Create(spawn.transform);
                instance.Init();
                _barrels.Add(instance);
            }
        }

        public void Respawn()
        {
            for (int i = 0; i < _spawns.Length; i++)
            {
                MoveToSpawn(_barrels[i], _spawns[i]);
                _barrels[i].Init();
            }

            return;

            void MoveToSpawn(Barrel barrel, BarrelSpawn spawn)
            {
                barrel.transform.position = spawn.transform.position;
            }
        }
    }
}