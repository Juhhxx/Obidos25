using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour
{
    [SerializeField] private List<Language> _availableLanguages;
    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField, ReadOnly] private int _selectedLanguage;

    private int SelectedLanguage
    {
        get => _selectedLanguage;

        set
        {
            if (value != _selectedLanguage)
            {
                LocalizationManager.Language = _availableLanguages[value];
                PlayerPrefs.SetInt(SELECTEDLANG, value);
                PlayerPrefs.Save();
            }

            _selectedLanguage = value;
        }
    }

    private const string SELECTEDLANG = "selectedLangIdx";

    private void Awake()
    {
        SetUpLanguageMenu();
    }

    private void SetUpLanguageMenu()
    {
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();

        foreach (Language lang in _availableLanguages)
        {
            optionDatas.Add(new TMP_Dropdown.OptionData(lang.DisplayName, lang.Flag, Color.white));
        }

        _selectedLanguage = optionDatas.Count;

        _languageDropdown.AddOptions(optionDatas);

        SelectedLanguage = PlayerPrefs.GetInt(SELECTEDLANG);
        _languageDropdown.value = SelectedLanguage;
        _languageDropdown.RefreshShownValue();
    }

    public void ChangeLanguage()
    {
        SelectedLanguage = _languageDropdown.value;
    }
}
