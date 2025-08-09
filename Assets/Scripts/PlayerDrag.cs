using NaughtyAttributes;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private KeyCode _dragKey;
    [SerializeField][ReadOnly] private Draggabble _curentDraggable;

    private Vector3 _cameraPos;
    private bool _isDragging = false;

    private void Start()
    {
        _cameraPos = Camera.main.transform.position;
    }
    private void Update()
    {
        if (!_isDragging) CheckForDraggable();

        if (_curentDraggable != null) Drag();
    }

    private void CheckForDraggable()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        RaycastHit2D hit = Physics2D.CircleCast(pos, 0.5f, Vector2.zero);

        Draggabble temp = hit.collider?.gameObject.GetComponent<Draggabble>();

        Debug.Log($"COLLIDED WITH : {hit.collider?.gameObject.name}");

        if (temp != null) _curentDraggable = temp;
        else _curentDraggable = null;
    }

    private void Drag()
    {
        if (Input.GetKey(_dragKey))
        {
            _isDragging = true;
            _curentDraggable.FollowMouse();
        }
        else _isDragging = false;
    }


    private void OnDrawGizmos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        Gizmos.color = _curentDraggable == null ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(pos, 0.5f);
    }
}
