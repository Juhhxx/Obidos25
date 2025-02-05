using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public void SetUpCard(Military military)
    {
        // Photo
        Image photo = transform.GetChild(0).GetComponent<Image>();

        photo.sprite = military.Picture;

        // ID
        TextMeshProUGUI ID = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        ID.text = military.ID;

        // Name
        TextMeshProUGUI name = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        name.text = "\t" + military.Name;

        // Height
        TextMeshProUGUI height = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        height.text = military.Height + "cm";

        // Eye Color
        TextMeshProUGUI eye = transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        eye.text = military.EyeColor;

        // Division
        TextMeshProUGUI division = transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        division.text = military.Division.ToString();

        // Features
        TextMeshProUGUI features = transform.GetChild(7).GetComponent<TextMeshProUGUI>();

        features.text = military.Features;

        // Siganture
        Image signature = transform.GetChild(8).GetComponent<Image>();

        signature.sprite = military.Signature;
    }
}
