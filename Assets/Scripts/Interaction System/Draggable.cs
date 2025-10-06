using System;
using UnityEngine;

public class Draggabble : Interactable
{
    private float _followSpeed => PlayerInteraction.Instance.DragFollowSpeed;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private Vector3 _mousePos;
    private Vector3 _offSet;
    private Vector3 _initialPos;

    private void Awake()
    {
        if (transform.parent?.GetComponentInParent<Draggabble>() == null)
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _initialPos = transform.position;
            LayerManager.Instance.RegisterDragable(this);
        }
    }

    private void OnEnable()
    {
        InteractBegin += SetUp;
        Interact += FollowMouse;
        InteractEnd += TurnOnCollider;
    }
    private void OnDisable()
    {
        
        InteractBegin   -= SetUp;
        Interact        -= FollowMouse;
        InteractEnd     -= TurnOnCollider;
    }
    private void OnDestroy()
    {
        LayerManager.Instance.UnregisterDragable(this);
    }

    private void Update()
    {
        _mousePos = PlayerInteraction.Instance.MousePosition;
        _mousePos.z = transform.position.z;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(_initialPos.x, _initialPos.y, -1f);

        GetComponent<CardItem>()?.ToggleCardItemSprite(true);
    }


    private void SetUp()
    {
        Debug.Log($"{name} : START DRAGGING");
        // Stop Gravity form Affecting Draggable while dragging it.
        _rb.linearVelocityY = 0f;
        // Deactivate Collider to Stop from being triggered by Gravity Trigger
        _collider.enabled = false;

        CalculateOffset();
    }
    private void TurnOnCollider()
    {
        _collider.enabled = true;
    }

    public void ResetOffSet() => _offSet = Vector3.zero;
    private void CalculateOffset()
    {
        _offSet = transform.position - _mousePos;
        _offSet.z = 0f;
        Debug.Log($"OFFSET : {_offSet}");
    }

    private void FollowMouse()
    {
        Vector3 pos = _mousePos + _offSet;

        transform.position = transform.position + (pos - transform.position) * _followSpeed;
    }
}
