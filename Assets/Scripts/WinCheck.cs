using UnityEngine;
using UnityEngine.UI;
using Obidos25;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    public void SetMilitary(List<Military> militaryList) => _militaryList = new List<Military>(militaryList);

    private List<Military> _suspects = new List<Military>();

    private int _numberOfMoles;
    public void SetMoles(int num) => _numberOfMoles = num;

    [SerializeField] private GameObject _portaits;
    [SerializeField] private Sprite _markedSprite;
    [SerializeField] private TextMeshProUGUI _bufoNumber;

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void SetPortaits()
    {
        _bufoNumber.text = $"Can you identify the {_numberOfMoles} mole(s) attending the event?";
        
        for (int i = 0; i < _portaits.transform.childCount; i++)
        {
            if (i >= _militaryList.Count) continue;

            Military m = _militaryList[i];

            GameObject child = _portaits.transform.GetChild(i).gameObject;

            SetUpAccusationCard setUp = child.GetComponent<SetUpAccusationCard>();

            string[] names = _militaryList[i].Name.Split(" ");

            string shortName = names[0] + " " + names[names.Count() - 1];

            setUp.SetCard(m.Picture, shortName, m.IsMarked, m.SuspicionLevel, _markedSprite);
            
            UnityEngine.UI.Button btt = child.GetComponent<UnityEngine.UI.Button>();

            int idx = i;
            btt.onClick.AddListener(() => SelectSuspect(idx));
        }
    }

    public void SelectSuspect(int index)
    {
        Military m = _militaryList[index];

        GameObject child = _portaits.transform.GetChild(index).gameObject;

        SetUpAccusationCard setUp = child.GetComponent<SetUpAccusationCard>();


        if (_suspects.Contains(m))
        {
            _suspects.Remove(m);
            setUp.ToogleSelection(false);
        }
        else 
        {
            _suspects.Add(m);
            setUp.ToogleSelection(true);
        }
    }

    public void CheckBufo()
    {
        bool right = true;

        foreach (Military suspect in _suspects)
        {
            if (!suspect.IsMole)
            {
                right = false;
                break;
            }
        }
        
        if (right)
            _winScreen.SetActive(true);
        else
            _loseScreen.SetActive(true);
    }

    public void StartFinal()
    {
        _finalScreen.SetActive(true);
    }
}
