using UnityEngine;
using UnityEngine.UI;
using Obidos25;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private List<Military> _militaryList;
    public void SetMilitary(List<Military> militaryList) => _militaryList = new List<Military>(militaryList);

    private List<Military> _suspects = new List<Military>();

    private int _numberOfMoles;
    public void SetMoles(int num, List<Military> moles)
    {
        _numberOfMoles = num;

        TextMeshProUGUI tmp = _cheat.GetComponentInChildren<TextMeshProUGUI>();

        tmp.text = "";

        foreach (Military m in moles)
        {
            tmp.text += m.GetShortName() + "\n";
        }   
    }

    [SerializeField] private GameObject _portaits;
    [SerializeField] private Sprite _markedSprite;
    [SerializeField] private TextMeshProUGUI _bufoNumber;
    [SerializeField] private Animator _bufoNumberAnim;
    [SerializeField] private GameObject _cheat;
    [SerializeField] private bool _allowCheat;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _accusationScreen;
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private Cutscene _winCutscene;
    [SerializeField] private Cutscene _loseCutscene;

    public void SetPortaits()
    {
        string mole = _numberOfMoles > 1 ? "moles" : "mole";

        _bufoNumber.text = $"Can you identify the\n<b>{_numberOfMoles} {mole}</b> attending\nthe event?";
        
        for (int i = 0; i < _portaits.transform.childCount; i++)
        {
            if (i >= _militaryList.Count) continue;

            Military m = _militaryList[i];

            GameObject child = _portaits.transform.GetChild(i).gameObject;

            SetUpAccusationCard setUp = child.GetComponent<SetUpAccusationCard>();

            setUp.SetCard(m.Picture, m.GetShortName(), m.IsMarked, m.SuspicionLevel, _markedSprite);
            
            UnityEngine.UI.Button btt = child.GetComponent<UnityEngine.UI.Button>();

            int idx = i;
            btt.onClick.AddListener(() => SelectSuspect(idx));
        }
    }

    public void SelectSuspect(int index)
    {
        MenuManager.Instance.ResetSelection();
        
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
        MenuManager.Instance.ResetSelection();
        
        if (_suspects.Count != _numberOfMoles)
        {
            _bufoNumberAnim.SetTrigger("Warn");
            return;
        }
        
        bool right = true;

        foreach (Military suspect in _suspects)
        {
            if (!suspect.IsMole)
            {
                right = false;
                break;
            }
        }
        
        _blackScreen.SetActive(true);

        if (right)
            CutsceneManager.Instance.PlayCutscene(_winCutscene, () =>
            {
                MenuManager.Instance.LoadScene("MainMenu", () => CreditsManager.Instance.OpenCredits(false));
            });
        else
            CutsceneManager.Instance.PlayCutscene(_loseCutscene, () => MenuManager.Instance.LoadScene("MainMenu"));
    }

    private void MoleCheat()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
        {
            _cheat.SetActive(!_cheat.activeInHierarchy);
        }
    }

    private void Update()
    {
        if (_allowCheat) MoleCheat();
    }

    public void StartFinal()
    {
        _accusationScreen.SetActive(true);
    }
}
