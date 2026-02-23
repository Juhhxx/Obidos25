using System;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using Yarn.Unity;

public class LocalizedComponent : MonoBehaviour
{

    [Header("Asset Definitions")]
    [SerializeField] private LocalizationType _assetType;


    [Header("Localization Variations")]
    [SerializeField, ShowIf("_assetType", LocalizationType.TMP)]
    private List<LocalizedText> _localizationsTexts;

    [Header("Localization Variations")]
    [SerializeField, ShowIf("IsSprite")]
    private List<LocalizedSprite> _localizationsSprites;

    private bool IsSprite =>    _assetType == LocalizationType.SpriteRenderer || 
                                _assetType == LocalizationType.Image;

    [Header("Localization Variations")]
    [SerializeField, ShowIf("_assetType", LocalizationType.ButtonSprites)]
    private List<LocalizedButtonSprites> _localizationsButtonSprites;

    [Header("Localization Variations")]
    [SerializeField, ShowIf("_assetType", LocalizationType.YarnProject)]
    private List<LocalizedYarnProject> _localizationsProjects;

    private void OnEnable()
    {
        UpdateLocalizedComponent(LocalizationManager.Language);
        LocalizationManager.OnLanguageChanged += UpdateLocalizedComponent;
    }

    private void OnDisable()
    {
        LocalizationManager.OnLanguageChanged -= UpdateLocalizedComponent;
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
            
            case LocalizationType.ButtonSprites:
                UnityEngine.UI.Button btt = GetComponent<UnityEngine.UI.Button>();
                UpdateAsset(btt, lang);
                break;
            
            case LocalizationType.YarnProject:
                DialogueRunner runner = GetComponent<DialogueRunner>();
                UpdateAsset(runner, lang);
                break;
        }
    }

    private void UpdateAsset(TextMeshProUGUI tmp, Language lang) =>
    tmp.text = LocalizedAssets.GetLocalization<LocalizedText>(lang, _localizationsTexts, gameObject).Text;

    private void UpdateAsset(Image image, Language lang) =>
    image.sprite = LocalizedAssets.GetLocalization<LocalizedSprite>(lang, _localizationsSprites, gameObject).Sprite;

    private void UpdateAsset(SpriteRenderer spr, Language lang) =>
    spr.sprite = LocalizedAssets.GetLocalization<LocalizedSprite>(lang, _localizationsSprites, gameObject).Sprite;

    private void UpdateAsset(UnityEngine.UI.Button button, Language lang)
    {
        button.image.sprite = LocalizedAssets.GetLocalization<LocalizedButtonSprites>(lang, _localizationsButtonSprites, gameObject).SpriteNormal;

        SpriteState spriteState = new SpriteState();

        spriteState.highlightedSprite = 
        LocalizedAssets.GetLocalization(lang, _localizationsButtonSprites, gameObject).SpriteHighlighted;
        spriteState.pressedSprite = 
        LocalizedAssets.GetLocalization(lang, _localizationsButtonSprites, gameObject).SpritePressed;

        button.spriteState = spriteState;
    }

    private void UpdateAsset(DialogueRunner tmp, Language lang) =>
    tmp.SetProject(LocalizedAssets.GetLocalization<LocalizedYarnProject>(lang, _localizationsProjects, gameObject).Project);

}
