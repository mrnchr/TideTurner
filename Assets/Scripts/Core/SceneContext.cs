using UnityEngine;

public class SceneContext : MonoBehaviour
{
    private void Awake()
    {
        var ctx = FindAnyObjectByType<ProjectContext>();
        if (!ctx)
            ctx = Instantiate(Resources.Load<ProjectContext>("Core"));

        var level = FindAnyObjectByType<Level>();
        if (level)
        {
            level.Construct();
            level.Init();
        }
    }
}