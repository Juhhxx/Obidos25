using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Scriptable Objects/Language")]
public class Language : ScriptableObject
{
    public string DisplayName;
    public string Code;
    public Sprite Flag;
}
