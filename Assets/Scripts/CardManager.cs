using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _full;
    private bool isItem = true;
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
    }
    public void ToggleCardItemSprite()
    {
        Debug.Log("TOGGLE CARD");
        
        isItem = !isItem;

        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!isItem);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(isItem);
        
    }
}
