using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public class BarrelContainer : MonoBehaviour
    {
        private readonly List<Barrel> _barrels = new List<Barrel>();
    
        [SerializeField] private Barrel _prefab;
        [SerializeField] private Transform _parent;
    
        private BarrelFactory _factory;
        private BarrelSpawn[] _spawns;

        public void Construct(
            BarrelSpawn[] spawns,
            AbstractMoonData moonData,
            LevelUpdater updater,
            WaterMovement waterMovement,
            global::Muchachos.TideTurner.Runtime.Level.Level level)
        {
            _factory = new BarrelFactory(_prefab, _parent, moonData, updater, waterMovement, level);
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