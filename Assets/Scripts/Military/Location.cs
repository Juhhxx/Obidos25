using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Scriptable Objects/Location")]
public class Location : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField][ReadOnly] private int _idRegion;
    public int IdRegion => _idRegion;

    public void UpdateRegion(int idRegion) => _idRegion = idRegion;
    
}
