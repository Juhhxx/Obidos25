using UnityEngine;

public class Draggabble : Interactable
{
    private float _followSpeed => PlayerInteraction.Instance.DragFollowSpeed;
    private Rigidbody2D _rb;

    private void Start()
    {
        transform.position += -Vector3.forward;

        _rb = GetComponent<Rigidbody2D>();
    }
    public void FollowMouse()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;

        transform.position = transform.position + (pos - transform.position) * _followSpeed;

        _rb.linearVelocityY = 0f; // Stop Gravity form Affecting Draggable while dragging it.
    }
}
