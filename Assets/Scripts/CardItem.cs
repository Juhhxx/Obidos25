using NaughtyAttributes;
using UnityEngine;

public class CardItem : MonoBehaviour
{
    [OnValueChanged("UpdateImage")]
    [SerializeField] private bool _isItem;

    BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void ToggleCardItemSprite()
    {
        Debug.Log("TOGGLE CARD");

        _isItem = !_isItem;

        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!_isItem);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(_isItem);

        int activeChild = _isItem ? 1 : 0;

        UpdateColliderSize(transform.GetChild(activeChild).GetComponent<SpriteRenderer>().sprite);

    }
    public void ToggleCardItemSprite(bool state)
    {
        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!state);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(state);

        int activeChild = _isItem ? 1 : 0;

        UpdateColliderSize(transform.GetChild(activeChild).GetComponent<SpriteRenderer>().sprite);
    }

    private void UpdateImage() => ToggleCardItemSprite(_isItem);

    private void UpdateColliderSize(Sprite sprite)
    {
        if (sprite == null) return;

        if (_collider == null) _collider = GetComponent<BoxCollider2D>();

        Vector2 newS = sprite.bounds.size;

        _collider.size = newS;
    }
}

