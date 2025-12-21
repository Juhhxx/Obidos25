using System;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;

public class LocalizedComponent : MonoBehaviour
{

    [Header("Asset Definitions")]
    [SerializeField] private LocalizationType _assetType;

    

    [Header("Localization Variations")]
    [SerializeField, ShowIf("_assetType", LocalizationType.TMP)]
    private List<LocalizedText> _localizationsTexts;

    [Header("Localization Variations")]
    [SerializeField, HideIf("_assetType", LocalizationType.TMP)]
    private List<LocalizedSprite> _localizationsSprites;

    private void OnEnable()
    {
        UpdateLocalizedComponent(LocalizationManager.Instance?.Language);
        LocalizationManager.Instance.OnLanguageChanged += UpdateLocalizedComponent;
    }

    private void OnDisable()
    {
        if (LocalizationManager.Instance == null) return;
        
        LocalizationManager.Instance.OnLanguageChanged -= UpdateLocalizedComponent;
    }

    public void UpdateLocalizedComponent(Language lang)
    {
        if (lang == null)
        {
            Debug.LogWarning($"[Localization System] Error : Language given was NULL", this);
            return;
        }

        switch (_assetType)
        {
            case LocalizationType.TMP:
                TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
                UpdateAsset(tmp, lang);
                break;
            
            case LocalizationType.Image:
                Image img = GetComponent<Image>();
                UpdateAsset(img, lang);
                break;

            case LocalizationType.SpriteRenderer:
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                UpdateAsset(sr, lang);
                break;
        }
    }

    private void UpdateAsset(TextMeshProUGUI tmp, Language lang) =>
    tmp.text = LocalizedAssets.GetLocalization<LocalizedText>(lang, _localizationsTexts, gameObject).Text;

    private void UpdateAsset(Image image, Language lang) =>
    image.sprite = LocalizedAssets.GetLocalization<LocalizedSprite>(lang, _localizationsSprites, gameObject).Sprite;

    private void UpdateAsset(SpriteRenderer sr, Language lang) =>
    sr.sprite = LocalizedAssets.GetLocalization<LocalizedSprite>(lang, _localizationsSprites, gameObject).Sprite;
}
