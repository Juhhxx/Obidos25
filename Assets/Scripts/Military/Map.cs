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

    public Location GetRandomLocationInOtherRegion(int index)
    {
        var tmp = new List<Region>(_regions);

        tmp.RemoveAt(index - 1);

        int rnd = Random.Range(0, tmp.Count);

        return tmp[rnd].GetRandomLocation();
    }
}
