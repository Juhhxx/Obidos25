using System;
using NaughtyAttributes;
using UnityEngine;

public static class LocalizationManager
{
    [SerializeField] private static Language _language;

    public static Language Language
    {
        get
        {
            if (_language == null)
                Debug.LogWarning($"[Localization System] Warning : No Language Selected!");

            return _language;
        }

        set
        {
            if (value != _language)
            {
                OnLanguageChanged?.Invoke(value);
                Debug.LogWarning($"[Localization System] Updated Language to {value.DisplayName}");
            }

            _language = value;
        }
    }

    public static event Action<Language> OnLanguageChanged;
}
