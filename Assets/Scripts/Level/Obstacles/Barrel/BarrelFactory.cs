using UnityEngine;

public class BarrelFactory
{
    private readonly Barrel _prefab;
    private readonly Transform _parent;
    private readonly MoonData _moonData;
    private readonly LevelUpdater _updater;

    public BarrelFactory(Barrel prefab, Transform parent, MoonData moonData, LevelUpdater updater)
    {
        _prefab = prefab;
        _parent = parent;
        _moonData = moonData;
        _updater = updater;
    }

    public Barrel Create(Transform spawn)
    {
        Barrel instance = Object.Instantiate(_prefab, spawn.position, Quaternion.identity, _parent);
        instance.Construct(_moonData);
        _updater.Add(instance);

        return instance;
    }
}