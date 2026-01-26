using UnityEngine;
using System;
using System.Collections.Generic;

public class CursorSpriteState : MonoBehaviour
{
    public enum CursorState { Default, Interact, Hold, Cliked }

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


    private Vector2 pos = Vector2.zero;
    private CursorState _currentState = CursorState.Default;

    public CursorState CurrentState
    {
        get => _currentState;

        set
        {
            if (value != _currentState)
            {
                _spr.sprite = GetSpriteState(value);
                pos = PlayerInteraction.Instance.MousePosition;
            }

            _currentState = value;
        }
    }

    bool posChanged = false;

    private void UpdateState()
    {
        Interactable cur = PlayerInteraction.Instance.CurrentInteractable;

        if (cur == null)
        {
            CurrentState = CursorState.Default;

            posChanged = false;

            return;
        }

        if (PlayerInteraction.Instance.IsInteracting)
        {
            if (cur is Button)
            {
                if (posChanged) CurrentState = CursorState.Hold;
                else CurrentState = CursorState.Cliked;
                
                if (Vector2.Distance(PlayerInteraction.Instance.MousePosition, pos) > 0.25f)
                {
                    posChanged = true;
                }
            }
            else if (cur is Draggabble)
            {
                CurrentState = CursorState.Hold;
            }
        }
        else
        {
            CurrentState = CursorState.Interact;
            posChanged = false;
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
