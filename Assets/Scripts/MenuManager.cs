using System.Collections.Generic;
using NaughtyAttributes;
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

    private Animator _anim;

    private void Awake()
    {
        base.SingletonCheck(this, true);

        _anim = GetComponent<Animator>();
    }

    public void Quit() => Application.Quit();

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }
    public void ResetSelection() => EventSystem.current.SetSelectedGameObject(null);

    private void CheckPause()
    {
        if (_noPauseScenes.Contains(SceneManager.GetActiveScene().name)) return;

        if (_pauseMenu.activeInHierarchy) return;

        if (Input.GetKeyDown(_pauseKey))
        {
            TooglePauseMenu(true);
        }
    }

    public void TooglePauseMenu(bool onOff)
    {
        _pauseMenu.SetActive(onOff);
        AudioManager.Instance.TogglePauseAllGroups(onOff);
        ResetSelection();

        if (onOff)
        {
            Time.timeScale = 0f;
        }
        else Time.timeScale = 1f;
    }
    public void ToogleOptionsMenu(bool onOff)
    {
        if (onOff) _anim.SetTrigger("OpenOptions");
        else _anim.SetTrigger("CloseOptions");
    }
    public void ToogleInstructionsMenu(bool onOff) => _instructionsMenu.SetActive(onOff);
    public void ToogleConfirmQuitMenu(bool onOff) => _confirmQuitMenu.SetActive(onOff);
    public void ToogleConfirmMainMenu(bool onOff) => _confirmMainMenu.SetActive(onOff);

    private void Update()
    {
        CheckPause();
    }
}
