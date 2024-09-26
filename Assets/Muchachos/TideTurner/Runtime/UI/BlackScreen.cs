using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BlackScreen : MonoBehaviour
{
    private const float Accuracy = 0.001f;
    private const float Delay = 0.5f;
    private const int SortingLayer = 32767 - 1;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private RawImage _blackScreenImage;

    private bool _isEnable;
    private bool _isCoroutineWorking;
    private int _sign;
    private float _timer;
    private GlobalSceneLoader _globalSceneLoader;
    private Color FixedDeltaColor;

    [Inject]
    public void Construct(GlobalSceneLoader globalSceneLoader)
    {
        FixedDeltaColor = new Color(0, 0, 0, Time.fixedDeltaTime);

        _globalSceneLoader = globalSceneLoader;

        _canvas.sortingOrder = SortingLayer;

        _globalSceneLoader.AddToBeforeLoadingQueue(SetBlack);
        _globalSceneLoader.AddToBeforeLoadingQueue(FadeIn);

        //_globalSceneLoader.AddToAfterLoadingQueue(SetBlack);
        //_globalSceneLoader.AddToAfterLoadingQueue(FadeIn);

        FadeIn();
    }

    public void FadeIn()
    {
        _isEnable = true;
        _sign = -1;
    }

    public void FadeOut()
    {
        _isEnable = true;
        _sign = 1;
    }

    public void SetBlack() => _blackScreenImage.color = Color.black;

    private void FixedUpdate()
    {
        HandleImageAlpha();
    }

    private void HandleImageAlpha()
    {
        if (!_isEnable)
        {
            _timer = 0;
            return;
        }

        if (_timer < Delay)
        {
            _timer += Time.fixedDeltaTime;
            return;
        }

        _blackScreenImage.color += _sign * FixedDeltaColor;

        _isEnable = _blackScreenImage.color.a is > Accuracy or < 1 - Accuracy;
    }
}