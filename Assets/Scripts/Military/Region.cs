using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System;

[Serializable]
public class Region
{
    [SerializeField] private int _regionID;
    public int RegionID => _regionID;

    [SerializeField][Expandable] private List<Location> _locations;
    public List<Location> Locations => _locations;

    public void UpdateID(int id)
    {
        Debug.Log("update");
        _regionID = id;
    }

    public void UpdateLocations()
    {
        Debug.Log("UPDATING LOCATIONS IDS");

        for (int i = 0; i < _locations.Count; i++) _locations[i].UpdateRegion(_regionID);
    }

}
