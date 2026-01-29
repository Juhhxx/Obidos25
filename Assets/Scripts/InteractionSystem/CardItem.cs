using NaughtyAttributes;
using Obidos25;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Draggabble))]
public class CardItem : MonoBehaviour
{
    [OnValueChanged("UpdateImage")]
    [SerializeField] private bool _isItem;

    public bool IsItem => _isItem;

    [SerializeField] private GameObject _fullCard;
    [SerializeField][Layer] private string _fullLayer;

    [SerializeField] private GameObject _itemCard;
    [SerializeField][Layer] private string _itemLayer;

    BoxCollider2D _collider;
    SpriteRenderer _rendererFull;
    SpriteRenderer _rendererItem;
    Draggabble _drag;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _drag = GetComponent<Draggabble>();

        _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
        _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Changes the object between full form and item form.
    /// </summary>
    /// <param name="state">Which form to take, true for item, false for full.</param>
    public void ToggleCardItemSprite(bool state)
    {
        if (_rendererFull == null || _rendererItem == null)
        {
            _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
            _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
        }

        if (_collider == null) _collider = GetComponent<BoxCollider2D>();

        _isItem = state;

        // Full Sprite
        _fullCard.gameObject.SetActive(!state);

        // Item Sprite
        _itemCard.gameObject.SetActive(state);

        SpriteRenderer activeRenderer = state ? _rendererItem : _rendererFull;

        string layer = state ? _itemLayer : _fullLayer;

        gameObject.layer = LayerMask.NameToLayer(layer);

        _collider.UpdateColliderBasedOnSprite(activeRenderer.sprite);

        _drag?.ResetOffSet();
    }

    private void UpdateImage() => ToggleCardItemSprite(_isItem);
    
}

