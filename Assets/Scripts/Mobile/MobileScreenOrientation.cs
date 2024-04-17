using System;
using UnityEngine;

public class MobileScreenOrientation : MonoBehaviour
{
    public event Action OnScreenOrientationChange;

    private MoonData _data;
    private InputController _inputController;
    private CameraMovement _cameraMovement;
    private ScreenOrientation _temp;
    private void OnDisable()
    {
        _inputController.OnInputHandled -= UpdateLogic;
        OnScreenOrientationChange -= _data.Init;
    }

    public void Construct(MoonData data, InputController inputController, CameraMovement cameraMovement)
    {
        _data = data;
        _inputController = inputController;
        _cameraMovement = cameraMovement;
        
        _inputController.OnInputHandled += UpdateLogic;
        OnScreenOrientationChange += _data.Init;
    }

    private void UpdateLogic(InputData inputData)
    {
        HandleOrientationData();
    }
    
    private void HandleOrientationData()
    {
        if (Screen.orientation != _temp )
        {
            OnScreenOrientationChange?.Invoke();
        }

        _cameraMovement.ChangeOrthographicSize(Screen.orientation == ScreenOrientation.Portrait
            ? OrthographicSizeType.PORTAIT
            : OrthographicSizeType.NORMAL);

        _temp = Screen.orientation;
    }
}
