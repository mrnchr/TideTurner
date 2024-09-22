using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationController : MonoBehaviour
{
    [SerializeField] private LocalizationSettings _localizationSettings;
    [SerializeField] private Locale _ruLocale;
    [SerializeField] private Locale _engLocale;

    private bool _isInitialized;

    public void SetLang(string lang)
    {
        StartCoroutine(Initialize(lang));
    }

    private IEnumerator Initialize(string lang)
    {
        if (!_isInitialized)
            yield return LocalizationSettings.InitializationOperation;

        _isInitialized = true;

        HandleLang(lang);
    }

    private void HandleLang(string lang)
    {
        switch (lang)
        {
            case "ru":
                _localizationSettings.SetSelectedLocale(_ruLocale);
                break;
            case "en":
                _localizationSettings.SetSelectedLocale(_engLocale);
                break;
            default:
                _localizationSettings.SetSelectedLocale(_engLocale);
                break;
        }
    }
}
