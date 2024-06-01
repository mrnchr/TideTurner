using DefaultNamespace.Core;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _startButton;
    private ButtonSoundCaller _buttonSoundCaller;

    private void Start()
    {
        _startButton = GetComponent<Button>();
        _buttonSoundCaller = GetComponent<ButtonSoundCaller>();

        SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
        
        if (!Application.isMobilePlatform)
            _startButton.onClick.AddListener(() =>
            {
                _buttonSoundCaller.PlaySound();
                sceneLoader.LoadScene(sceneLoader.PCscene);
            });
        else
            _startButton.onClick.AddListener(() =>
            {
                _buttonSoundCaller.PlaySound();
                sceneLoader.LoadScene(sceneLoader.MobileScene);
            });
    }
}