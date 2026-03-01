using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Scriptable Objects/Language")]
public class Language : ScriptableObject
{
    public string DisplayName;
    public LanguageCode Code;
    public Sprite Flag;
}

public enum LanguageCode
{
    Pt,
    Eng
}
