using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [SerializeField, Scene] private List<string> _noPauseScenes;
    [SerializeField] private KeyCode _pauseKey;
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(string scene) => SceneManager.LoadScene(scene);

    private void CheckPause()
    {
        if (_noPauseScenes.Contains(SceneManager.GetActiveScene().name)) return;

        if (_pauseMenu.activeInHierarchy) return;

        if (Input.GetKeyDown(_pauseKey))
        {
            _pauseMenu.SetActive(true);
        }
    }

    private void Update()
    {
        CheckPause();
    }
}
