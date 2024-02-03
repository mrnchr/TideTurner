using UnityEngine;

public class SceneContext : MonoBehaviour
{
    private void Awake()
    {
        var ctx = FindAnyObjectByType<ProjectContext>();
        if (!ctx)
            ctx = Instantiate(Resources.Load<ProjectContext>("Core"));

        var bootstrap = FindAnyObjectByType<LevelBootstrap>();
        if (bootstrap)
        {
            bootstrap.Construct();
            bootstrap.Init();
        }
    }
}