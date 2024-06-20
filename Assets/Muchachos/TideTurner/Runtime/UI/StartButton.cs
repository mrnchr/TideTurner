using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class StartButton : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void OnClick()
        {
            _sceneLoader.LoadScene(Application.isMobilePlatform ? SceneType.MobileLevel : SceneType.PCLevel);
        }
    }
}