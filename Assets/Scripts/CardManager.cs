using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Military _military;

    public void SetUpCard()
    {
        // Photo
        Image photo = transform.GetChild(0).GetComponent<Image>();

        photo.sprite = _military.Picture[0];

        // ID
        TextMeshProUGUI ID = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        ID.text = _military.ID;

        // Name
        TextMeshProUGUI name = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        name.text = "\t" + _military.Name;

        // Height
        TextMeshProUGUI height = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        height.text = _military.Height + "cm";

        // Eye Color
        TextMeshProUGUI eye = transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        eye.text = _military.EyeColor;

        // Division
        TextMeshProUGUI division = transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        division.text = _military.Division.ToString();

        // Features
        TextMeshProUGUI features = transform.GetChild(7).GetComponent<TextMeshProUGUI>();

        features.text = _military.Features;

        // Siganture
        Image signature = transform.GetChild(8).GetComponent<Image>();

        signature.sprite = _military.Signature;
    }
}
