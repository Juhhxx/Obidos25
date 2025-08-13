using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Division", menuName = "Scriptable Objects/Division")]
public class Division : ScriptableObject
{
    [OnValueChanged("UpdateName")]
    [SerializeField] private string _divisionName;
    public string DivisionName => _divisionName;

    [ShowAssetPreview]
    [SerializeField] private Sprite _divisionBadge;
    public Sprite DivisionBadge => _divisionBadge;

    public char RegimentLetter => _divisionName[0];

}
