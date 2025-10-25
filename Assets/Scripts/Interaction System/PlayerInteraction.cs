using NaughtyAttributes;
using UnityEngine;

public class PlayerInteraction : MonoBehaviourSingleton<PlayerInteraction>
{
    [Header("Configuration Values")]
    [Space(5f)]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private BoundingBox _cursorBoundingBox;

    [Space(10f)]
    [Header("Input Values")]
    [Space(5f)]
    [SerializeField][InputAxis] private string _interactButton;

    [SerializeField] private float _dragFollowSpeed = 0.35f;
    public float DragFollowSpeed => _dragFollowSpeed;

    [Space(10f)]
    [Header("Debug Values")]
    [Space(5f)]
    [SerializeField][ReadOnly] private bool _isInteracting = false;
    public bool IsInteracting => _isInteracting;

    [SerializeField][ReadOnly] private Interactable _curentInteractable;
    public Interactable CurrentInteractable => _curentInteractable;

    public void SetInteractable(Interactable interactable)
    {
        _curentInteractable = interactable;

        _curentInteractable.OnInteractBegin();

        ((Draggabble)_curentInteractable).ResetOffSet();
    }
    public void ResetInteractable()
    {
        _isInteracting = false;
        _curentInteractable = null;
    }

    public Vector3 MousePosition => _cursor.transform.position;

    public bool IsInsideBoundings => _cursorBoundingBox.CheckIfInside(MousePosition);

    private void Awake()
    {
        base.SingletonCheck(this);
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        MoveCursor();

        if (IsInsideBoundings)
        {
            if (!_isInteracting) CheckForInteractable();

            if (_curentInteractable != null) Interact();
        }
        else
        {
            _curentInteractable?.OnInteractEnd();
            _curentInteractable = null;
            _isInteracting = false;
        }
    }

    private void MoveCursor()
    {
        Vector3 pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = _cursor.transform.position.z;

        _cursor.transform.position = pos;
    }

    private void CheckForInteractable()
    {
        Vector3 pos = MousePosition;
        pos.z = -10f;

        Ray ray = new Ray(pos, Vector3.forward);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10f);

        Interactable temp = hit.collider?.gameObject.GetComponent<Interactable>();

        // Debug.Log($"COLLIDED WITH : {hit.collider?.gameObject.name}");

        if (temp != null)
        {
            _curentInteractable = temp;
        }
        else _curentInteractable = null;
    }

    private void Interact()
    {
        if (Input.GetButtonDown(_interactButton))
        {
            _curentInteractable?.OnInteractBegin();
            _isInteracting = true;
        }
        else if (Input.GetButton(_interactButton))
        {
            _curentInteractable?.OnInteract();
            _isInteracting = true;
        }
        else if (Input.GetButtonUp(_interactButton))
        {
            _curentInteractable?.OnInteractEnd();
            _isInteracting = true;
        }
        else _isInteracting = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = MousePosition;

        Gizmos.color = _curentInteractable == null ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(pos, 1f);
        Gizmos.DrawLine(pos, pos + (Vector3.forward * -10));
    }
}
