using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [SerializeField] private KeyCode _pauseKey;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mainMenuButton;

    private void Awake()
    {
        base.SingletonCheck(this, true);
    }

    public void Quit() => Application.Quit();

    public void LoadScene(string scene) => SceneManager.LoadScene(scene);

    private void CheckPause()
    {
        if (_pauseMenu.activeInHierarchy) return;

        if (Input.GetKeyDown(_pauseKey))
        {
            _pauseMenu.SetActive(true);

            bool inMainMenu = SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0);

            _mainMenuButton.SetActive(!inMainMenu);
        }
    }

    private void Update()
    {
        CheckPause();
    }
}
