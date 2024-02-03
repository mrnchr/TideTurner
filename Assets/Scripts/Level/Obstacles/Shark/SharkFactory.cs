using UnityEngine;

public class SharkFactory
{
    private readonly Shark _prefab;
    private readonly Transform _parent;
    private readonly Water _water;
    private readonly LevelUpdater _updater;
    private readonly Level _level;

    public SharkFactory(Shark prefab, Transform parent, Water water, LevelUpdater updater, Level level)
    {
        _prefab = prefab;
        _parent = parent;
        _water = water;
        _updater = updater;
        _level = level;
    }

    public Shark Create(Transform spawn)
    {
        var instance = Object.Instantiate(_prefab, spawn.transform.position, Quaternion.identity, _parent);
        instance.Construct();
        
        instance.GetComponent<Obstacle>().Construct(_level);
        
        var being = instance.GetComponent<WaterBeing>();
        being.Construct(_water);
        _updater.Add(being);

        return instance;
    }
}