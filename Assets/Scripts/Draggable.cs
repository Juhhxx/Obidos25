using UnityEngine;

public class Draggabble : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 0.05f;
    public void FollowMouse()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        transform.position = transform.position + (pos - transform.position) * _followSpeed;
    }
}
