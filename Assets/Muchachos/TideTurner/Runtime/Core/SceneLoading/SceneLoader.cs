using Muchachos.TideTurner.Runtime.Configuration;
using UnityEngine.SceneManagement;

namespace Muchachos.TideTurner.Runtime.Core.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly SceneConfig _scenes;

        public SceneLoader(IConfigProvider configProvider)
        {
            _scenes = configProvider.Get<SceneConfig>();
        }
        
        public void LoadScene(SceneType id)
        {
            SceneManager.LoadScene(_scenes.Get(id));
        }
    }
}