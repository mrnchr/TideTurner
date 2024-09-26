namespace Muchachos.TideTurner.Runtime.Core.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private GlobalSceneLoader _globalSceneLoader;

        public SceneLoader(GlobalSceneLoader globalSceneLoader)
        {
            _globalSceneLoader = globalSceneLoader;
        }

        public void LoadScene(SceneType id)
        {
            _globalSceneLoader.LoadScene(id);
        }
    }
}