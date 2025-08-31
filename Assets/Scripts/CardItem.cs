using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Draggabble))]
public class CardItem : MonoBehaviour
{
    [OnValueChanged("UpdateImage")]
    [SerializeField] private bool _isItem;

    [SerializeField] private GameObject _fullCard;
    [SerializeField][Layer] private string _fullLayer;

    [SerializeField] private GameObject _itemCard;
    [SerializeField][Layer] private string _itemLayer;

    BoxCollider2D _collider;
    SpriteRenderer _rendererFull;
    SpriteRenderer _rendererItem;
    Draggabble _drag;

    [SerializeField][ReadOnly] private static CardItem _lastSelectedItem;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _drag = GetComponent<Draggabble>();

        _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
        _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        _drag.InteractBegin += UpdateSelected;
    }
    private void OnDisable()
    {
        _drag.InteractBegin -= UpdateSelected;
    }

    public void ToggleCardItemSprite(bool state)
    {
        if (_rendererFull == null || _rendererItem == null)
        {
            _rendererFull = _fullCard.GetComponent<SpriteRenderer>();
            _rendererItem = _itemCard.GetComponent<SpriteRenderer>();
        }

        _isItem = state;

        // Full Sprite
        _fullCard.gameObject.SetActive(!state);

        // Item Sprite
        _itemCard.gameObject.SetActive(state);

        SpriteRenderer activeRenderer = state ? _rendererItem : _rendererFull;
        string layer = state ? _itemLayer : _fullLayer;

        gameObject.layer = LayerMask.NameToLayer(layer);

        UpdateColliderSize(activeRenderer.sprite);
        UpdateRendererLayer("Selected");
    }

    private void UpdateImage() => ToggleCardItemSprite(_isItem);

    private void UpdateColliderSize(Sprite sprite)
    {
        if (sprite == null) return;

        if (_collider == null) _collider = GetComponent<BoxCollider2D>();

        Vector2 newS = sprite.bounds.size;

        _collider.size = newS;

        _drag?.ResetOffSet();
    }

    public void UpdateRendererLayer(string layer)
    {
        _rendererFull.sortingLayerName = layer;
        _rendererItem.sortingLayerName = layer;

        SpriteRenderer[] aditionalRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (aditionalRenderers != null || aditionalRenderers.Length != 0)
        {
            foreach (SpriteRenderer r in aditionalRenderers)
            {
                r.sortingLayerName = layer;
            }
        }

        Canvas canvas = GetComponentInChildren<Canvas>();

        if (canvas != null)
        {
            canvas.sortingLayerName = layer;
        }
    }

    public void UpdateSelected()
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

