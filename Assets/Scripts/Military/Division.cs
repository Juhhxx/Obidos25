using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Division", menuName = "Scriptable Objects/Division")]
public class Division : Badges
{
    public char RegimentLetter => _name[0];

}
