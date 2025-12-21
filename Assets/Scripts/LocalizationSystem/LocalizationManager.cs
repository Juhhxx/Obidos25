using System;
using NaughtyAttributes;
using UnityEngine;

public class LocalizationManager : MonoBehaviourSingleton<LocalizationManager>
{
    [OnValueChanged("Bruh")]
    [SerializeField] private Language _language;

    private void Bruh() => Language = _language;

    public Language Language
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

    public event Action<Language> OnLanguageChanged;

    private void Awake()
    {
        base.SingletonCheck(this);
    }
}
