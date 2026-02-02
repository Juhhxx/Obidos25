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
    [SerializeField] private PlaySound _fullSound;

    [SerializeField] private GameObject _itemCard;
    [SerializeField][Layer] private string _itemLayer;
    [SerializeField] private PlaySound _itemSound;

    [SerializeField] private PlaySound _dropSound;

    BoxCollider2D _collider;
    SpriteRenderer _rendererFull;
    SpriteRenderer _rendererItem;
    Draggabble _drag;
    BookPageManager _book;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _drag = GetComponent<Draggabble>();
        _book = GetComponentInChildren<BookPageManager>();

        _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
        _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Changes the object between full form and item form.
    /// </summary>
    /// <param name="state">Which form to take, true for item, false for full.</param>
    public void ToggleCardItemSprite(bool state, bool playSound = true)
    {
        if (_rendererFull == null || _rendererItem == null)
        {
            _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
            _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
        }

        if (_collider == null) _collider = GetComponent<BoxCollider2D>();


        if (state == _isItem) playSound = false;

        _isItem = state;

        // Full Sprite
        _fullCard.gameObject.SetActive(!state);

        // Item Sprite
        _itemCard.gameObject.SetActive(state);

        if (playSound)
        {
            if (state)
            {
                _itemSound?.SoundPlay();
            }
            else
            {
                _fullSound?.SoundPlay();
            }
        }

        SpriteRenderer activeRenderer = state ? _rendererItem : _rendererFull;

        string layer = state ? _itemLayer : _fullLayer;

        gameObject.layer = LayerMask.NameToLayer(layer);

        _collider.UpdateColliderBasedOnSprite(activeRenderer.sprite);

        _drag?.ResetOffSet();
        if (!state) _book?.Reset();
    }

    private void UpdateImage() => ToggleCardItemSprite(_isItem);

    public void PlayDropSound() => _dropSound?.SoundPlay();
    
}

