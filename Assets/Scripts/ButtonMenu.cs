using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private string _buttonOne;
    [SerializeField] private string _buttonTwo;

    public void ButtonOne()
    {
        SceneManager.LoadScene(_buttonOne);
    }
    public void ButtonTwo()
    {
        SceneManager.LoadScene(_buttonTwo);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
