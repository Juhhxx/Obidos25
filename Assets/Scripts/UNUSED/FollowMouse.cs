using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 0.05f;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        transform.position = transform.position + (pos - transform.position) * _speed;
    }
}
