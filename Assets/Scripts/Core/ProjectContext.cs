using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}