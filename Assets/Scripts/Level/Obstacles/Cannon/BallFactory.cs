using UnityEngine;

public class BallFactory
{
    private readonly Ball _prefab;
    private readonly Transform _parent;

    public BallFactory(Ball prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public Ball Create()
    {
        Ball instance = Object.Instantiate(_prefab, _parent);
        instance.Construct();
        return instance;
    }
}