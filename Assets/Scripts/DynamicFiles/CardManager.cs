using Obidos25;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _portrait;
    [SerializeField] private SpriteRenderer _signature;
    [SerializeField] private TextMeshProUGUI _ID;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _height;
    [SerializeField] private TextMeshProUGUI _eyeColor;
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _division;
    [SerializeField] private TextMeshProUGUI _regiment;
    [SerializeField] private TextMeshProUGUI _facialFeatures;

    private Vector3 _initialPos; 

    private void Awake()
    {
        _initialPos = transform.position;
    }
    public void SetUpCard(Military military)
    {
        // Photo
        _portrait.sprite = military.Picture;

        // Siganture
        _signature.sprite = military.Signature;

        // ID
        _ID.text = military.ID;

        // Name
        _name.text = military.Name;

        // Height
        _height.text = military.Height + "cm";

        // Eye Color
        _eyeColor.text = military.EyeColor;

        // Division
        _division.text = military.Division.DivisionName;

        // Features
        _facialFeatures.text = military.Features;

        // Rank
        _rank.text = military.Rank.RankName;

        // Regiment
        _regiment.text = military.Regiment;

        // Reset Position and State
        transform.position = _initialPos;
    }
}
