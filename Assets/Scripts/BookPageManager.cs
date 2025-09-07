using UnityEngine;
using System;
using System.Collections.Generic;
using Obidos25;

public class BookPageManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _bookPages;

    [SerializeField] private Collider2D _pageForwardButton;
    [SerializeField] private Collider2D _pageBackwardButton;

    private int _currentPageIndex = 0;

    private SpriteRenderer _bookSR;
    private BoxCollider2D _collider;

    private void Start()
    {
        _bookSR = GetComponent<SpriteRenderer>();
        _collider = GetComponentInParent<BoxCollider2D>();

        _pageBackwardButton.enabled = false;
    }

    public void ChangePage(bool backwards)
    {
        int newPageIndex = backwards ? _currentPageIndex - 1 : _currentPageIndex + 1;

        if (newPageIndex < 0 || newPageIndex >= _bookPages.Count) return;

        _pageForwardButton.enabled = true;
        _pageBackwardButton.enabled = true;

        _bookSR.sprite = _bookPages[newPageIndex];

        _collider.UpdateColliderBasedOnSprite(_bookSR.sprite);

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
}
