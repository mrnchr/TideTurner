using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muchachos.TideTurner.Runtime.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public string PCScene = "Level 1";
        public string MobileScene = "MobileScene";
        
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}