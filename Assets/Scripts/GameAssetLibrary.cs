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
    [SerializeField] private List<Badges> _rankBadges;
    public List<Badges> RankBadges => _rankBadges;

    [SerializeField] private List<Badges> _divisionBadges;
    public List<Badges> DivisionBadges => _divisionBadges;

    public Badges GetWrongBadge(Badges correctBadge, bool rank)
    {
        List<Badges> badgeList = rank ? _rankBadges : _divisionBadges;

        badgeList.Remove(correctBadge);

        int rnd = Random.Range(0, badgeList.Count);

        return badgeList[rnd]; 
    }

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
