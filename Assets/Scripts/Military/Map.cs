using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    [OnValueChanged("UpdateRegions")]
    [SerializeField] private List<Region> _regions;

    private void UpdateRegions()
    {
        Debug.Log("UPDATING REGIONS");
        for (int i = 0; i < _regions.Count; i++)
        {
            _regions[i].UpdateID(i + 1);

            _regions[i].UpdateLocations();
        }
    }
}
