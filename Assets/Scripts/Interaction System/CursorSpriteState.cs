using UnityEngine;
using System;
using System.Collections.Generic;

public class CursorSpriteState : MonoBehaviour
{
    public enum CursorState { Default, Interact, Click, Hold }

    [Serializable]
    public struct StateSprite
    {
        [field: SerializeField] public CursorState CursorState;
        [field: SerializeField] public Sprite Sprite;
    }

    [SerializeField] private List<StateSprite> _cursorStates;

    private Sprite GetSpriteState(CursorState state)
    {
        foreach (StateSprite ss in _cursorStates)
        {
            if (ss.CursorState == state) return ss.Sprite;
        }

        return null;
    }

    private SpriteRenderer _spr;

    private CursorState _currentState = CursorState.Default;

    public CursorState CurrentState
    {
        get => _currentState;

        set
        {
            if (value != _currentState)
            {
                _spr.sprite = GetSpriteState(value);
            }

            _currentState = value;
        }
    }

    private void UpdateState()
    {
        Interactable cur = PlayerInteraction.Instance.CurrentInteractable;

        if (cur == null)
        {
            CurrentState = CursorState.Default;

            return;
        }

        if (PlayerInteraction.Instance.IsInteracting)
        {
            if (cur is Button)
            {
                CurrentState = CursorState.Click;
            }
            else if (cur is Draggabble)
            {
                CurrentState = CursorState.Hold;
            }
        }
        else
        {
            CurrentState = CursorState.Interact;
        }
    }

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        UpdateState();
    }
}
