using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class CardItem : MonoBehaviour
{
    [OnValueChanged("UpdateImage")]
    [SerializeField] private bool _isItem;

    BoxCollider2D _collider;
    SpriteRenderer _rendererFull;
    SpriteRenderer _rendererItem;
    Draggabble _drag;

    [SerializeField][ReadOnly] private static CardItem _lastSelectedItem;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _drag = GetComponent<Draggabble>();

        _rendererFull = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _rendererItem = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _drag.OnSelected.AddListener(UpdateSelected);
    }
    private void OnDisable()
    {
        _drag.OnSelected.RemoveListener(UpdateSelected);
    }

    public void ToggleCardItemSprite()
    {
        Debug.Log("TOGGLE CARD");

        _isItem = !_isItem;

        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!_isItem);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(_isItem);

        SpriteRenderer activeRenderer = _isItem ? _rendererItem : _rendererFull;

        UpdateColliderSize(activeRenderer.sprite);
    }
    public void ToggleCardItemSprite(bool state)
    {
        // Full Sprite
        transform.GetChild(0).gameObject.SetActive(!state);

        // Item Sprite
        transform.GetChild(1).gameObject.SetActive(state);

        SpriteRenderer activeRenderer = state ? _rendererItem : _rendererFull;

        UpdateColliderSize(activeRenderer.sprite);
    }

    private void UpdateImage() => ToggleCardItemSprite(_isItem);

    private void UpdateColliderSize(Sprite sprite)
    {
        if (sprite == null) return;

        if (_collider == null) _collider = GetComponent<BoxCollider2D>();

        Vector2 newS = sprite.bounds.size;

        _collider.size = newS;
    }
    public void UpdateRendererLayer(string layer)
    {
        _rendererFull.sortingLayerName = layer;
        _rendererItem.sortingLayerName = layer;
    }

    private void UpdateSelected()
    {
        if (_lastSelectedItem == this) return;

        Debug.Log("UPDATE SELECTED");
        
        if (_lastSelectedItem != null)
        {
            Debug.Log("REMOVE LAST SELECTECTED");

            var pos1 = _lastSelectedItem.transform.position;
            pos1.z = -1f;
            _lastSelectedItem.transform.position = pos1;
            _lastSelectedItem.UpdateRendererLayer("Default");
        }

        var pos2 = transform.position;
        pos2.z = -2f;
        transform.position = pos2;

        UpdateRendererLayer("Selected");
        _lastSelectedItem = this;
    }
}

