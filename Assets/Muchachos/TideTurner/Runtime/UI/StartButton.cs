using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class StartButton : MonoBehaviour
    {
        // DEBUG.
        [SerializeField] private bool isMobileScene;
        
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void OnClick()
        {
            _sceneLoader.LoadScene(Application.isMobilePlatform || isMobileScene ? SceneType.MobileLevel : SceneType.PCLevel);
        }
    }
}