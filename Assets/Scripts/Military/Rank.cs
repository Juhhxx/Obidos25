using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Rank", menuName = "Scriptable Objects/Rank")]
public class Rank : ScriptableObject
{
    [OnValueChanged("UpdateName")]
    [SerializeField] private string _rankName;
    public string RankName => _rankName;

    [ShowAssetPreview]
    [SerializeField] private Sprite _rankBadge;
    public Sprite RankBadge => _rankBadge;
}
