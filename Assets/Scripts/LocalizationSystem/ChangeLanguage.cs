using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private Language _language;

    public void SetUpLanguage(Language lang)
    {
        _language = lang;
    }

    public void SetLanguage()
    {
        LocalizationManager.Language = _language;
    }
}
