using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingScreen : MonoBehaviour
{
    private const int SortingLayer = 32767;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _scrollbar;

    private GlobalSceneLoader _sceneLoader;

    [Inject]
    public void Construct(GlobalSceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;

        _canvas.sortingOrder = SortingLayer;
    }

    private void Update()
    {
        _scrollbar.value = _sceneLoader.LoadingProgress;
    }
}