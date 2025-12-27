using UnityEngine;
using System;
using System.Collections.Generic;
using Obidos25;

public class BookPageManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _bookPages;

    [SerializeField] private Collider2D _backToStartButton;
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
        if (_backToStartButton != null) _backToStartButton.enabled = false;
    }

    public void SetPageSprite(int page, Sprite spr) 
    {
        _bookPages[page] = spr;

        if (page == _currentPageIndex) ChangePage(page);
    }

    private void ChangePage(int page)
    {
        if (page < 0 || page >= _bookPages.Count) return;

        _pageForwardButton.enabled = true;
        _pageBackwardButton.enabled = true;
        
        if (_backToStartButton != null) _backToStartButton.enabled = true;

        _bookSR.sprite = _bookPages[page];

        _collider.UpdateColliderBasedOnSprite(_bookSR.sprite);

        if (page == 0)
        {
            _pageBackwardButton.enabled = false;
            Debug.Log("FIRST PAGE, HIDING BACKWORDS BUTTON");
            
            if (_backToStartButton != null) _backToStartButton.enabled = false;
        }
        else if (page == _bookPages.Count - 1)
        {
            _pageForwardButton.enabled = false;
            Debug.Log("LAST PAGE, HIDING FORWARDS BUTTON");
        }

        if (page == 1)
        {
            if (_backToStartButton != null)  _backToStartButton.enabled = false;
        }

        _currentPageIndex = page;
    }

    public void FlipPage(bool backwards)
    {
        int newPageIndex = backwards ? _currentPageIndex - 1 : _currentPageIndex + 1;

        ChangePage(newPageIndex);
    }

    public void GoToFirstPage() => ChangePage(1);
}
