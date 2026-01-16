using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class ButtonMenu : MonoBehaviour
{
    [Scene]
    [SerializeField] private string _buttonOne;
    
    [Scene]
    [SerializeField] private string _buttonTwo;

    public void ButtonOne()
    {
        SceneManager.LoadScene(_buttonOne);
    }
    public void ButtonTwo()
    {
        SceneManager.LoadScene(_buttonTwo);
    }
    public void OpenOptions()
    {
        MenuManager.Instance.ToogleOptionsMenu(true);
    }
    public void OpenInstructions()
    {
        MenuManager.Instance.ToogleInstructionsMenu(true);
    }
    public void Quit()
    {
        MenuManager.Instance.Quit();
    }

    public void ResetSelection()
    {
        MenuManager.Instance.ResetSelection();
    }

    public void OpenEPICWELink()
    {
        Application.OpenURL("https://epic-we.eu/");
    }
}
