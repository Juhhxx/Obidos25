using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        transform.position = pos;
    }
}
