using UnityEngine;

public class CardItem : MonoBehaviour
{
    [SerializeField] private bool _isItem;
    public void ToggleCardItemSprite()
    {
        Debug.Log("TOGGLE CARD");
        
        _isItem = !_isItem;

        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!_isItem);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(_isItem);
        
    }
    public void ToggleCardItemSprite(bool state)
    {
        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!state);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(state);
    }
}

