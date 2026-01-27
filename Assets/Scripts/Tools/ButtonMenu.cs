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
        MenuManager.Instance.LoadScene(_buttonOne);
    }
    public void ButtonTwo()
    {
        MenuManager.Instance.LoadScene(_buttonTwo);
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

    public void OpenCredits()
    {
        CreditsManager.Instance.OpenCredits();
    }
}
