using NaughtyAttributes;
using UnityEngine;

public class PlayerInteraction : MonoBehaviourSingleton<PlayerInteraction>
{
    [SerializeField] private float _dragFollowSpeed = 0.35f;
    public float DragFollowSpeed => _dragFollowSpeed;
    private bool _isDragging = false;

    [SerializeField] private KeyCode _dragKey;
    [SerializeField][ReadOnly] private Interactable _curentInteractable;

    private Vector3 _cameraPos;

    private void Awake()
    {
        base.SingletonCheck(this);
    }

    private void Start()
    {
        _cameraPos = Camera.main.transform.position;
    }
    private void Update()
    {
        if (!_isDragging) CheckForInteractable();

        if (_curentInteractable != null) Interact();
    }

    private void CheckForInteractable()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = new Ray(pos, Vector3.forward);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10f);

        Draggabble temp = hit.collider?.gameObject.GetComponent<Draggabble>();

        Debug.Log($"COLLIDED WITH : {hit.collider?.gameObject.name}");

        if (temp != null)
        {
            _curentInteractable = temp;
            Debug.DrawLine(pos, hit.point, Color.blue);
        }
        else _curentInteractable = null;

        Debug.DrawLine(pos, pos + (Vector3.forward * 10f), Color.red);
    }

    private void Interact()
    {
        if (_curentInteractable is Draggabble)
        {
            if (Input.GetKey(_dragKey))
            {
                _isDragging = true;
                Draggabble temp = _curentInteractable as Draggabble;
                temp.OnSelected?.Invoke();
                temp.FollowMouse();
            }
            else _isDragging = false;
        }
    }


    private void OnDrawGizmos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        Gizmos.color = _curentInteractable == null ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(pos, 0.5f);
    }
}
