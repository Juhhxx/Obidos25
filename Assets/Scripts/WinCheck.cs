using UnityEngine;
using Obidos25;

public class WinCheck : MonoBehaviour
{
    private Military _mole;
    public Military Mole { get; set; }
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public void CheckBufo(Military military)
    {
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
