using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour
{
    [SerializeField] private List<Language> _availableLanguages;
    [SerializeField] private GameObject _languageButtonPrefab;
    [SerializeField] private Transform _languageButtonParent;
    [SerializeField] private Image _currentLanguageUIImage;

    private void Start()
    {
        UpdateIcon();
        SetUpLanguageMenu();
    }

    private void SetUpLanguageMenu()
    {
        foreach (Language lang in _availableLanguages)
        {
            GameObject newButton = Instantiate(_languageButtonPrefab, _languageButtonParent);

            SetUpLanguageButton setUp = newButton.GetComponent<SetUpLanguageButton>();
            ChangeLanguage changeLang = newButton.GetComponent<ChangeLanguage>();
            UnityEngine.UI.Button button = newButton.GetComponent<UnityEngine.UI.Button>();

            setUp.SetUpButton(lang.Flag, lang.DisplayName);
            changeLang.SetUpLanguage(lang);

            button.onClick.AddListener(changeLang.SetLanguage);
            button.onClick.AddListener(UpdateIcon);
        }
    }

    private void UpdateIcon()
    {
        _currentLanguageUIImage.sprite = LocalizationManager.Language.Flag;
    }
}
