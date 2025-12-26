using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [SerializeField, Scene] private List<string> _noPauseScenes;
    [SerializeField] private KeyCode _pauseKey;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _instructionsMenu;
    [SerializeField] private GameObject _confirmQuitMenu;
    [SerializeField] private GameObject _confirmMainMenu;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(string scene) => SceneManager.LoadScene(scene);

    public void ResetSelection() => EventSystem.current.SetSelectedGameObject(null);

    private void CheckPause()
    {
        if (_noPauseScenes.Contains(SceneManager.GetActiveScene().name)) return;

        if (_pauseMenu.activeInHierarchy) return;

        if (Input.GetKeyDown(_pauseKey))
        {
            _pauseMenu.SetActive(true);
        }
    }

    public void TooglePauseMenu(bool onOff)
    {
        _pauseMenu.SetActive(onOff);
        ResetSelection();
    }
    public void ToogleOptionsMenu(bool onOff) => _optionsMenu.SetActive(onOff);
    public void ToogleInstructionsMenu(bool onOff) => _instructionsMenu.SetActive(onOff);
    public void ToogleConfirmQuitMenu(bool onOff) => _confirmQuitMenu.SetActive(onOff);
    public void ToogleConfirmMainMenu(bool onOff) => _confirmMainMenu.SetActive(onOff);

    private void Update()
    {
        CheckPause();
    }
}
