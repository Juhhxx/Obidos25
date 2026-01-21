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

    private bool _canPause = true;
    public bool CanPause { get => _canPause; set => _canPause = value; }

    private bool _optionsOpen = false;
    public bool OptionsOpen => _optionsOpen;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();

        _anim.enabled = false;
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _instructionsMenu.SetActive(false);
        _confirmQuitMenu.SetActive(false);
        _confirmMainMenu.SetActive(false);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(string scene)
    {
        _anim.enabled = false;

        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        SceneChanger.Instance.ChangeScene(scene);
    }
    public void ResetSelection() => EventSystem.current.SetSelectedGameObject(null);

    private void CheckPause()
    {
        if (!_canPause) return;

        if (_noPauseScenes.Contains(SceneManager.GetActiveScene().name)) return;

        if (_pauseMenu.activeInHierarchy) return;

        if (Input.GetKeyDown(_pauseKey))
        {
            TooglePauseMenu(true);
        }
    }

    public void TooglePauseMenu(bool onOff)
    {
        AudioManager.Instance.TogglePauseAllGroups(onOff);
        ResetSelection();

        _anim.enabled = true;

        if (onOff)
        {
            Time.timeScale = 0f;
            _anim.SetTrigger("OpenPause");
        }
        else
        {
            Time.timeScale = 1f;
            _anim.SetTrigger("ClosePause");
        }
    }
    public void ToogleOptionsMenu(bool onOff)
    {
        _anim.enabled = true;
        _optionsOpen = onOff;

        if (onOff) _anim.SetTrigger("OpenOptions");
        else _anim.SetTrigger("CloseOptions");
    }
    public void ToogleInstructionsMenu(bool onOff)
    {
        _anim.enabled = true;

        if (onOff) _anim.SetTrigger("OpenInstructions");
        else _anim.SetTrigger("CloseInstructions");
    }
    public void ToogleConfirmQuitMenu(bool onOff) => _confirmQuitMenu.SetActive(onOff);
    public void ToogleConfirmMainMenu(bool onOff) => _confirmMainMenu.SetActive(onOff);

    private void Update()
    {
        CheckPause();
    }
}
