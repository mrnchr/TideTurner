using UnityEngine;

public class SceneContext : MonoBehaviour
{
    private LevelBootstrap _bootstrap;

    private void Awake()
    {
        var ctx = FindAnyObjectByType<ProjectContext>();
        if (!ctx)
            ctx = Instantiate(Resources.Load<ProjectContext>("Core"));

        _bootstrap = FindAnyObjectByType<LevelBootstrap>();
        if (_bootstrap)
            _bootstrap.Construct();
    }

    private void Start()
    {
        if(_bootstrap)
            _bootstrap.Init();
    }
}