using UnityEngine;
using Obidos25;
using System.Collections.Generic;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private List<Military> _moles;
    public List<Military> Moles => _moles;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void CheckBufo(List<Military> suspects)
    {
        Debug.Log($"{suspects} = {_moles} ? {suspects == _moles}");

        if (suspects == _moles)
            _winScreen.SetActive(true);
        else
            _loseScreen.SetActive(true);
    }
    public void StartFinal()
    {
        _gameScreen.SetActive(false);
        _finalScreen.SetActive(true);
    }
}
