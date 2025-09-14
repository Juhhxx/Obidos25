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

    [SerializeField] private List<Military> _moles;
    public void SetMoles(List<Military> molesList) => _moles = new List<Military>(molesList);

    [SerializeField] private GameObject _portaits;
    [SerializeField] private TMP_InputField _accusationInputField;

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void SetPortaits()
    {
        for (int i = 0; i < _portaits.transform.childCount; i++)
        {
            GameObject child = _portaits.transform.GetChild(i).gameObject;

            Image img = child.GetComponent<Image>();

            img.sprite = _militaryList[i].Picture;

            if (_militaryList[i].IsMarked) img.color = Color.red;

            child.GetComponentInChildren<TextMeshProUGUI>().text = _militaryList[i].Name;
        }
    }

    public void CheckBufo(string suspects)
    {
        List<string> molesNames = new List<string>();

        foreach (Military m in _moles) molesNames.Add(m.Name);

        string[] suspectsNames = suspects.Split(", ");

        int numberRight = 0;

        foreach (string suspect in suspectsNames) if (molesNames.Contains(suspect)) numberRight++;
        
        if (numberRight == _moles.Count)
            _winScreen.SetActive(true);
        else
            _loseScreen.SetActive(true);
    }

    public void StartFinal()
    {
        _finalScreen.SetActive(true);
    }
}
