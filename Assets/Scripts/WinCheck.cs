using UnityEngine;
using Obidos25;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private Military _mole;
    public Military Mole
    {
        get => _mole;

        set => _mole = value;
    }
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void CheckBufo(Military military)
    {
        Debug.Log($"{military.Name} = {_mole.Name} ? {military == _mole}");
        if (military == _mole)
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
