using UnityEngine;

[CreateAssetMenu(fileName = "ParkingSpot", menuName = "Scriptable Objects/ParkingSpot")]
public class ParkingSpot : ScriptableObject
{
    [SerializeField] private string _spot;
    public string Spot => _spot;

    [SerializeField] private string _carPlate;
    public string CarPlate => _carPlate;
}
