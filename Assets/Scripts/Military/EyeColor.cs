using UnityEngine;

[CreateAssetMenu(fileName = "EyeColor", menuName = "Scriptable Objects/EyeColor")]
public class EyeColor : ScriptableObject
{
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    
}
