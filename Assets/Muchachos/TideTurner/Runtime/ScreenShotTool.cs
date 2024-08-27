using UnityEngine;

public class ScreenShotTool : MonoBehaviour
{
    [SerializeField] private string _screenShotPath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CaptureScreenShot();
        }
    }

    private void CaptureScreenShot()
    {
        string randomName = Random.Range(0, int.MaxValue).ToString();
        ScreenCapture.CaptureScreenshot(_screenShotPath + randomName + ".png");
    }
}
