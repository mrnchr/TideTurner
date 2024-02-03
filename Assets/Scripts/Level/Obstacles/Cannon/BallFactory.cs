using UnityEngine;

public class BallFactory
{
    private readonly Ball _prefab;
    private readonly Transform _parent;
    private readonly Level _level;

    public BallFactory(Ball prefab, Transform parent, Level level)
    {
        _prefab = prefab;
        _parent = parent;
        _level = level;
    }

    public Ball Create()
    {
        Ball instance = Object.Instantiate(_prefab, _parent);
        instance.Construct();
        instance.GetComponent<Obstacle>().Construct(_level);
        return instance;
    }
}