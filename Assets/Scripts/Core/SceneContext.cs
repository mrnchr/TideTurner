using UnityEngine;

namespace DefaultNamespace.Core
{
    public class SceneContext : MonoBehaviour
    {
        private void Awake()
        {
            var ctx = FindAnyObjectByType<ProjectContext>();
            if (!ctx)
                ctx = Instantiate(Resources.Load<ProjectContext>("Core"));
        }
    }
}