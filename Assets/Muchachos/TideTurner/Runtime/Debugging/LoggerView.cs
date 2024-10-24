using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Debugging
{
    public class LoggerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private RectTransform _textTransform;

        [SerializeField]
        private RectTransform _content;

        private ILoggerController _logger;

        [Inject]
        public void Construct(ILoggerController logger)
        {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            Destroy(gameObject);
#endif

            _logger = logger;
            _logger.OnLog += OnLog;
        }

        private void OnLog()
        {
            List<string> messages = new List<string>(_logger.GetMessages());
            messages.Reverse();
            _text.text = string.Join("\n", messages);
        }

        private void Update()
        {
            Vector2 size = _content.sizeDelta;
            size.y = _textTransform.sizeDelta.y;
            _content.sizeDelta = size;
        }

        private void OnDestroy()
        {
            _logger.OnLog -= OnLog;
        }
    }
}