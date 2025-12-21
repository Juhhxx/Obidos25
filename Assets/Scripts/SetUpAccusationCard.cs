using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUpAccusationCard : MonoBehaviour
{
    [SerializeField] private Image _photoImageUI;
    [SerializeField] private Image _polaroidOverlayImageUI;
    [SerializeField] private Image _selectionImageUI;
    [SerializeField] private TextMeshProUGUI _nameTMPUI;
    [SerializeField] private TextMeshProUGUI _suspicionLevelTMPUI;

    public void SetCard(Sprite photo, string name, bool suspect, int susLevel = 0, Sprite susOverlay = null)
    {
        _photoImageUI.sprite = photo;
        _nameTMPUI.text = name;
        _suspicionLevelTMPUI.text = "";
        _selectionImageUI.gameObject.SetActive(false);

        if (suspect)
        {
            _suspicionLevelTMPUI.text = susLevel.ToString();
            _polaroidOverlayImageUI.sprite = susOverlay;
        }
    }

    public void ToogleSelection(bool onOff)
    {
        Debug.Log($"{transform.parent.name} toogle {onOff}");
        _selectionImageUI.gameObject.SetActive(onOff);
    }

}
