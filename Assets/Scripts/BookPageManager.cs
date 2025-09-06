using UnityEngine;
using System;
using System.Collections.Generic;

public class BookPageManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _bookPages;

    [SerializeField] private Collider2D _pageForwardButton;
    [SerializeField] private Collider2D _pageBackwardButton;

    private int _currentPageIndex = 0;

    private SpriteRenderer _bookSR;

    private void Start()
    {
        _bookSR = GetComponent<SpriteRenderer>();
        _pageBackwardButton.enabled = false;
    }

    public void ChangePage(bool backwards)
    {
        int newPageIndex = backwards ? _currentPageIndex - 1 : _currentPageIndex + 1;

        if (newPageIndex < 0 || newPageIndex >= _bookPages.Count) return;

        _pageForwardButton.enabled = true;
        _pageBackwardButton.enabled = true;

        _bookSR.sprite = _bookPages[newPageIndex];

        UpdateColliderSize(_bookSR.sprite);

        if (newPageIndex == 0)
        {
            _pageBackwardButton.enabled = false;
        }
        else if (newPageIndex == _bookPages.Count - 1)
        {
            _pageForwardButton.enabled = false;
        }

        _currentPageIndex = newPageIndex;
    }

    private void UpdateColliderSize(Sprite sprite)
    {
        if (sprite == null) return;

        BoxCollider2D collider = GetComponentInParent<BoxCollider2D>();

        Vector2 newSize = sprite.bounds.size;
        Vector2 newOffset = sprite.bounds.center;

        collider.size = newSize;
        collider.offset = newOffset;
    }
}
