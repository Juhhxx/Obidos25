using NaughtyAttributes;
using UnityEngine;

public class PlayerInteraction : MonoBehaviourSingleton<PlayerInteraction>
{
    [SerializeField] private GameObject _cursor;
    [SerializeField][InputAxis] private string _interactButton;

    [SerializeField] private float _dragFollowSpeed = 0.35f;
    public float DragFollowSpeed => _dragFollowSpeed;
    private bool _isInteracting = false;

    [SerializeField][ReadOnly] private Interactable _curentInteractable;
    public Interactable CurrentInteractable => _curentInteractable;

    private Vector3 _cameraPos;

    public Vector3 MousePosition => _cursor.transform.position;

    private void Awake()
    {
        base.SingletonCheck(this);
    }

    private void Start()
    {
        Cursor.visible = false;
        _cameraPos = Camera.main.transform.position;
    }
    private void Update()
    {
        MoveCursor();

        if (!_isInteracting) CheckForInteractable();

        if (_curentInteractable != null) Interact();
    }

    private void MoveCursor()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        Debug.Log($"COLLIDED WITH : {hit.collider?.gameObject.name}");

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
            Debug.Log("INTERACTION BEGIN");
        }
        else if (Input.GetButton(_interactButton))
        {
            _curentInteractable?.OnInteract();
            _isInteracting = true;
            Debug.Log("INTERACTION");
        }
        else if (Input.GetButtonUp(_interactButton))
        {
            _curentInteractable?.OnInteractEnd();
            _isInteracting = true;
            Debug.Log("INTERACTION END");
        }
        else _isInteracting = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = MousePosition;

        Gizmos.color = _curentInteractable == null ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(pos, 0.5f);
        Gizmos.DrawLine(_cameraPos, pos);
    }
}
