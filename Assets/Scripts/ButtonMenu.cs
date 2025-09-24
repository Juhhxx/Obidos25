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
    public void Quit()
    {
        Application.Quit();
    }
}
