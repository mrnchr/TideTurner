using System.Runtime.InteropServices;

public class YandexGamesIntegration
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    /* DEBUG
    private void Start()
    {
        Debug.Log("Start called");
#if UNITY_WEBGL
        Hello();

        ShowAdv();
        RateGame();
#endif
        Debug.Log("Start call end");
    }
    */

    public void CallRateGameWindow()
    {
        RateGame();
    }

    public void CallAdvWindow()
    {
        ShowAdv();
    }
}