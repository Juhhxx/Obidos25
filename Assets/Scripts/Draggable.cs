using UnityEngine;

public class Draggabble : Interactable
{
    private float _followSpeed => PlayerInteraction.Instance.DragFollowSpeed;

    private void Start()
    {
        transform.position += -Vector3.forward;
    }
    public void FollowMouse()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;

        transform.position = transform.position + (pos - transform.position) * _followSpeed;
    }
}
