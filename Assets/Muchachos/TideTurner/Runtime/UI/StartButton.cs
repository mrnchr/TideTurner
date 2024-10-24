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
            _sceneLoader.LoadScene(
#if UNITY_ANDROID
                SceneType.MobileLevel
#elif UNITY_STANDALONE
                SceneType.PCLevel
#endif
            );
        }
    }
}