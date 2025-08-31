using UnityEngine;
using UnityEngine.Rendering;

public class Draggabble : Interactable
{
    private float _followSpeed => PlayerInteraction.Instance.DragFollowSpeed;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private CardItem _cardItem;

    private Vector3 _mousePos;
    private Vector3 _offSet;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _cardItem = GetComponent<CardItem>();
    }

    private void OnEnable()
    {
        InteractBegin += SetUp;
        Interact += FollowMouse;
        InteractEnd += TurnOnCollider;
    }
    private void OnDisable()
    {
        InteractBegin -= SetUp;
        Interact -= FollowMouse;
        InteractEnd -= TurnOnCollider;
    }

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = transform.position.z;
    }

    private void SetUp()
    {
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
