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
    public string InteractButton => _interactButton;

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
        _curentInteractable?.OnInteractEnd();
        _isInteracting = false;
        _curentInteractable = null;
    }

    public Vector3 MousePosition => _cursor.transform.position;
    [SerializeField][ReadOnly] private Vector3 _mousePosition;

    public bool IsInsideBoundings => _cursorBoundingBox.CheckIfInside(MousePosition);
    [SerializeField][ReadOnly] private bool _isInsideBounds;

    private void Awake()
    {
        base.SingletonCheck(this, false);
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        MoveCursor();

        _isInteracting = Input.GetButton(_interactButton);

        if (IsInsideBoundings)
        {
            if (_curentInteractable != null) Interact();

            if (!_isInteracting) CheckForInteractable();
        }
        else
        {
            ResetInteractable();
        }

        _mousePosition = MousePosition;
        _isInsideBounds = IsInsideBoundings;
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

        if (_curentInteractable is Button previousButton)
        {
            if (previousButton.HighlightSR != null) previousButton.HighlightSR.enabled = false;
        }
        
        if (temp != null)
        {
            if (temp is Button currentButton)
            {
                if (currentButton.HighlightSR != null) currentButton.HighlightSR.enabled = true;
            }
            _curentInteractable = temp;
        }
        else _curentInteractable = null;
    }

    private void Interact()
    {
        if (Input.GetButtonDown(_interactButton))
        {
            _curentInteractable?.OnInteractBegin();
        }
        else if (Input.GetButton(_interactButton))
        {
            _curentInteractable?.OnInteract();
        }
        else if (Input.GetButtonUp(_interactButton))
        {
            _curentInteractable?.OnInteractEnd();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = MousePosition;

        Gizmos.color = _curentInteractable == null ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(pos, 1f);
        Gizmos.DrawLine(pos, pos + (Vector3.forward * -10));
    }
}
