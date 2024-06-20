using System.Collections;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime
{
    public class DebugStuff : MonoBehaviour
    {
        [Range(0f, 10f)][SerializeField] private float delayUpdate = 2.5f;
    
        private string _myLog = "";
        private string _output;
        private bool _isEnabled;
    
        private void OnEnable()
        {
            if (_isEnabled == false)
                return;
        
            Application.logMessageReceived += Log;

        
            StartCoroutine(StartLog());
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _isEnabled = !_isEnabled;
            }
        }
    
        private IEnumerator StartLog()
        {
            while (true)
            {
                _myLog = "";
            
                Debug.Log($"Screen.orientation: {Screen.orientation}\n");
            
                Debug.Log($"Input.acceleration.x: {Input.acceleration.x}\n");
                Debug.Log($"Input.acceleration.y: {Input.acceleration.y}\n");
            
                yield return new WaitForSeconds(delayUpdate);
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
        
            _myLog = GUI.TextArea(new Rect(Screen.width / 2, 10, Screen.width / 4, Screen.height / 2), _myLog);
        }
    }
}