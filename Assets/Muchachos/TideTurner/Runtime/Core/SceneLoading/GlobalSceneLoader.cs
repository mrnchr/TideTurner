using System;
using System.Collections;
using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GlobalSceneLoader : MonoBehaviour, ISceneLoader
{
    private const float FixedDeltaTime = 0.02f;
    
    private readonly Queue<Action> _beforeLoadingQueue = new Queue<Action>();
    private readonly Queue<Action> _afterLoadingQueue = new Queue<Action>();

    public float LoadingProgress { get; private set; }

    private SceneConfig _scenes;

    [Inject]
    public void Construct(IConfigProvider configProvider)
    {
        _scenes = configProvider.Get<SceneConfig>();
    }

    public void AddToBeforeLoadingQueue(Action action)
    {
        _beforeLoadingQueue.Enqueue(action);
    }

    public void AddToAfterLoadingQueue(Action action)
    {
        _afterLoadingQueue.Enqueue(action);
    }

    public void LoadScene(SceneType id)
    {
        StartCoroutine(LoadSceneAsync(id));
    }

    private IEnumerator LoadSceneAsync(SceneType id)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scenes.Get(SceneType.Boot));
        
        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading boot... " + Time.timeScale);
            yield return new WaitForSeconds(FixedDeltaTime);
        }

        while (_beforeLoadingQueue.Count > 0)
        {
            _beforeLoadingQueue.Dequeue()?.Invoke();
        }

        asyncOperation = SceneManager.LoadSceneAsync(_scenes.Get(id));

        while (!asyncOperation.isDone)
        {
            LoadingProgress = asyncOperation.progress;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        while (_afterLoadingQueue.Count > 0)
        {
            _afterLoadingQueue.Dequeue()?.Invoke();
        }
    }
}