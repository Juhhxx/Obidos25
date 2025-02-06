using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _full;
    private Vector3 _initialPos; 
    private RectTransform _rectTrans;
    private CardItem _cardItem;

    private void Awake()
    {
        _cardItem = GetComponent<CardItem>();
        _rectTrans = GetComponent<RectTransform>();
        _initialPos = _rectTrans.anchoredPosition;
    }
    public void SetUpCard(Military military)
    {
        // Photo
        Image photo = _full.transform.GetChild(0).GetComponent<Image>();

        photo.sprite = military.Picture;

        // ID
        TextMeshProUGUI ID = _full.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        ID.text = military.ID;

        // Name
        TextMeshProUGUI name = _full.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        name.text = "\t" + military.Name;

        // Height
        TextMeshProUGUI height = _full.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        height.text = military.Height + "cm";

        // Eye Color
        TextMeshProUGUI eye = _full.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        eye.text = military.EyeColor;

        // Division
        TextMeshProUGUI division = _full.transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        division.text = military.Division.ToString();

        // Features
        TextMeshProUGUI features = _full.transform.GetChild(7).GetComponent<TextMeshProUGUI>();

        features.text = military.Features;

        // Siganture
        Image signature = _full.transform.GetChild(8).GetComponent<Image>();

        signature.sprite = military.Signature;

        // Rank
        TextMeshProUGUI rank = _full.transform.GetChild(9).GetComponent<TextMeshProUGUI>();

        rank.text = military.Rank;

        // Regiment
        TextMeshProUGUI reg = _full.transform.GetChild(10).GetComponent<TextMeshProUGUI>();

        reg.text = military.Regiment;

        // Reset Position and State
        _rectTrans.anchoredPosition = _initialPos;
        _cardItem.ToggleCardItemSprite(true);
    }
}
