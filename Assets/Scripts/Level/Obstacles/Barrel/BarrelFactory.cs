using UnityEngine;

public class BarrelFactory
{
    private readonly Barrel _prefab;
    private readonly Transform _parent;
    private readonly MoonData _moonData;
    private readonly LevelUpdater _updater;
    private readonly WaterMovement _waterMovement;
    private readonly Level _level;

    public BarrelFactory(Barrel prefab, Transform parent, MoonData moonData, LevelUpdater updater, WaterMovement waterMovement, Level level)
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
        instance.GetComponent<Obstacle>().Construct(_level);
        _updater.Add(instance);

        return instance;
    }
}