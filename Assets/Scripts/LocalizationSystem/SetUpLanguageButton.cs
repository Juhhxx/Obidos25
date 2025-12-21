using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUpLanguageButton : MonoBehaviour
{
    [SerializeField] private Image _flagUIImage;
    [SerializeField] private TextMeshProUGUI _languageUITMP;

    public void SetUpButton(Sprite flag, string language)
    {
        _flagUIImage.sprite = flag;
        _languageUITMP.text = language;
    }
}
