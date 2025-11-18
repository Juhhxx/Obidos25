using NaughtyAttributes;
using UnityEngine;

public abstract class Badges : ScriptableObject
{
    [SerializeField] protected string _name;
    public string Name => _name;

    [ShowAssetPreview]
    [SerializeField] protected Sprite _badge;
    public Sprite Badge => _badge;
}
