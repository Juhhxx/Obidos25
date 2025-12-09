using UnityEngine;
using UnityEngine.UI;
using Obidos25;
using System.Collections.Generic;
using TMPro;
using System;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    public void SetMilitary(List<Military> militaryList) => _militaryList = new List<Military>(militaryList);

    private List<Military> _suspects = new List<Military>();

    private int _numberOfMoles;
    public void SetMoles(int num) => _numberOfMoles = num;

    [SerializeField] private GameObject _portaits;
    [SerializeField] private TextMeshProUGUI _bufoNumber;

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void SetPortaits()
    {
        _bufoNumber.text = $"There are {_numberOfMoles} bufo(s)";
        
        for (int i = 0; i < _portaits.transform.childCount; i++)
        {
            GameObject child = _portaits.transform.GetChild(i).gameObject;

            Image[] imgs = child.GetComponentsInChildren<Image>();

            imgs[0].enabled = false;

            imgs[1].sprite = _militaryList[i].Picture;

            TextMeshProUGUI[] tmps = child.GetComponentsInChildren<TextMeshProUGUI>();

            tmps[0].text = _militaryList[i].Name;

            tmps[1].text = _militaryList[i].SuspicionLevel.ToString();

            if (_militaryList[i].IsMarked) imgs[1].color = Color.red;
            else tmps[1].text = "";
        }
    }

    public void SelectSuspect(int index)
    {
        Military m = _militaryList[index];

        GameObject child = _portaits.transform.GetChild(index).gameObject;

        Image[] imgs = child.GetComponentsInChildren<Image>();


        if (_suspects.Contains(m))
        {
            _suspects.Remove(m);
            imgs[0].enabled = false;
        }
        else 
        {
            _suspects.Add(m);
            imgs[0].enabled = true;
        }
    }

    public void CheckBufo()
    {
        int numberRight = 0;

        foreach (Military suspect in _suspects) if (suspect.IsMole) numberRight++;
        
        if (numberRight == _numberOfMoles)
            _winScreen.SetActive(true);
        else
            _loseScreen.SetActive(true);
    }

    public void StartFinal()
    {
        _finalScreen.SetActive(true);
    }
}
