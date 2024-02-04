using System.Collections.Generic;
using UnityEngine;

public class SharkContainer : MonoBehaviour
{
    private readonly List<Shark> _sharks = new List<Shark>(); 
    
    [SerializeField] private Shark _prefab;
    [SerializeField] private Transform _parent;
    private SharkFactory _factory;
    private SharkSpawn[] _spawns;

    public void Construct(SharkSpawn[] spawns, Water water, LevelUpdater updater, Level level)
    {
        _spawns = spawns;
        _factory = new SharkFactory(_prefab, _parent, water, updater, level);
    }

    public void Init()
    {
        foreach (SharkSpawn spawn in _spawns)
        {
            var instance = _factory.Create(spawn.transform);
            instance.Init();
            _sharks.Add(instance);
        }
    }

    public void Respawn()
    {
        for (int i = 0; i < _spawns.Length; i++)
            RespawnShark(_sharks[i], _spawns[i]);

        return;

        void RespawnShark(Shark shark, SharkSpawn spawn)
        {
            shark.transform.position = spawn.transform.position;
            shark.SetDirection(spawn.transform.right);
            shark.Init();
        }
    }
}