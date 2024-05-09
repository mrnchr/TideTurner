using System;
using UnityEngine;

public class MobileScreenOrientation : MonoBehaviour
{
    public event Action OnScreenOrientationChange;

    private AbstractMoonData _data;
    private InputController _inputController;
    private CameraMovement _cameraMovement;
    private ScreenOrientation _temp;

    public void Construct(AbstractMoonData data, InputController inputController, CameraMovement cameraMovement)
    {
        _data = data;
        _inputController = inputController;
        _cameraMovement = cameraMovement;
        
        _inputController.OnInputHandled += UpdateLogic;
        OnScreenOrientationChange += _data.Init;
    }
    
    private void OnDisable()
    {
        _inputController.OnInputHandled -= UpdateLogic;
        OnScreenOrientationChange -= _data.Init;
    }

    private void UpdateLogic(InputData inputData)
    {
        HandleOrientationData();

        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void HandleOrientationData()
    {
        if (Screen.orientation != _temp)
        {
            OnScreenOrientationChange?.Invoke();
        }

        _cameraMovement.ChangeOrthographicSize(
            Screen.orientation == ScreenOrientation.Portrait
            ? OrthographicSizeType.PORTAIT
            : OrthographicSizeType.NORMAL);

        _temp = Screen.orientation;
    }
}
