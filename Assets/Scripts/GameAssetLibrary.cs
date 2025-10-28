using System.Collections.Generic;
using Obidos25;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAssetLibrary", menuName = "Scriptable Objects/GameAssetLibrary")]
public class GameAssetLibrary : ScriptableObject
{
    [Header("Characters")]
    [Space(5)]
    [SerializeField] private List<Military> _militaryCharacters;
    public List<Military> MilitaryCharacters => _militaryCharacters;

    [Space(10)]
    [Header("Badges")]
    [Space(5)]
    [SerializeField] private List<Rank> _rankBadges;
    public List<Rank> RankBadges => _rankBadges;

    [SerializeField] private List<Division> _divisionBadges;
    public List<Division> DivisionBadges => _divisionBadges;

    [Space(10)]
    [Header("Passwords")]
    [Space(5)]
    [SerializeField] private PasswordCalendar _passwordsInfo;
    public PasswordCalendar PasswordsInfo => _passwordsInfo;

    [Space(10)]
    [Header("Locations")]
    [Space(5)]
    [SerializeField] private List<Location> _locations;
    public List<Location> Locatiaons => _locations;

    [Space(10)]
    [Header("Parking Spots")]
    [Space(5)]
    [SerializeField] private List<ParkingSpot> _parkingSpots;
    public List<ParkingSpot> ParkingSpots => _parkingSpots;
}
