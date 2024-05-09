using System.Collections;
using UnityEngine;

public class DebugStuff : MonoBehaviour
{
    private string _myLog = "";
    private string _output;
    private bool _isEnabled;
    private void OnEnable()
    {
        Application.logMessageReceived += Log;
        
        StartCoroutine(StartLog());
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            _isEnabled = !_isEnabled;
        }
    }
    
    private IEnumerator StartLog()
    {
        while (true)
        {
            _myLog = "";
            Debug.Log($"Screen.orientation: {Screen.orientation} Application.isMobilePlatform: {Application.isMobilePlatform}");
            Debug.Log($"rotationRateUnbiased: {Input.gyro.rotationRateUnbiased}");
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void Log(string logString, string stackTrace, LogType type)
    {
        _output = logString;
        _myLog = _output + " " + _myLog;
        if (_myLog.Length > 5000)
        {
            _myLog = _myLog[..4000];
        }
    }

    private void OnGUI()
    {
        if (_isEnabled == false)
            return;
        
        _myLog = GUI.TextArea(new Rect(10, 10, Screen.width / 2, Screen.height / 4), _myLog);
    }
}