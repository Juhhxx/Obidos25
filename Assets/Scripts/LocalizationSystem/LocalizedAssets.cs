using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Yarn.Unity;

// Classes that define a text/sprite/etc and what language it represents
public abstract class LocalizedAssets
{
    [field: AllowNesting]
    [field: SerializeField] public Language Language { get; protected set; }

    public static T GetLocalization<T>(Language lang, List<T> localizations, GameObject go) where T : LocalizedAssets
    {
        T localization = default;

        foreach (T l in localizations)
        {
            if (l.Language == lang) localization = l;
        }

        if (localization == null)
            Debug.LogWarning($"[Localization System] Error : {go.name} does not have localization for {lang.DisplayName}!", go);
        else
            Debug.LogWarning($"[Localization System] Successfuly updated {go.name} to {lang.DisplayName}", go);
        
        return localization;
    }

    public static T GetLocalization<T>(List<T> localizations, GameObject go) where T : LocalizedAssets
    {
        return GetLocalization<T>(LocalizationManager.Language, localizations, go);
    }
}

[Serializable]
public class LocalizedText : LocalizedAssets
{
    [field: SerializeField, ResizableTextArea] public string Text { get; private set; }

    public LocalizedText(string text, Language language)
    {
        Text = text;
        Language = language;
    }
}

[Serializable]
public class LocalizedSprite : LocalizedAssets
{
    [field: SerializeField, ShowAssetPreview] public Sprite Sprite { get; private set; }

    public void UpdateSprite(Sprite spr) => Sprite = spr;

    public LocalizedSprite(Sprite sprite, Language language)
    {
        Sprite = sprite;
        Language = language;
    }
}

[Serializable]
public class LocalizedButtonSprites : LocalizedAssets
{
    [field: SerializeField, ShowAssetPreview] public Sprite SpriteNormal { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite SpriteHighlighted { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite SpritePressed { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite SpriteSelected { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite SpriteDeactivated { get; private set; }

    public LocalizedButtonSprites(Sprite spriteNor, Sprite spriteHigh, Sprite spritePress, Sprite spriteSel, Sprite spriteDect, Language language)
    {
        SpriteNormal = spriteNor;
        SpriteHighlighted = spriteHigh;
        SpritePressed = spritePress;
        SpriteSelected = spriteSel;
        SpriteDeactivated = spriteDect;

        Language = language;
    }
}

[Serializable]
public class LocalizedScriptableObject<T> : LocalizedAssets where T : ScriptableObject
{
    [field: AllowNesting]
    [field: SerializeField] public T ScriptableObject { get; private set; }

    public LocalizedScriptableObject(T scriptableObject, Language language)
    {
        ScriptableObject = scriptableObject;

        Language = language;
    }
}

[Serializable]
public class LocalizedYarnProject : LocalizedAssets
{
    [field: SerializeField] public YarnProject Project { get; private set; }

    public LocalizedYarnProject(YarnProject project, Language language)
    {
        Project = project;

        Language = language;
    }
}