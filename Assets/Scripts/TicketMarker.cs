using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class TicketMarker : MonoBehaviour
{
    [SerializeField][ReadOnly] private Ticket _currentTicket;

    [SerializeField] private Vector3 _pivotOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pivotDebugCrossSize;

    private Vector3 _pivot => transform.position + _pivotOffset;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Ticket ticket = other.gameObject.GetComponent<Ticket>();

        if (ticket != null)
        {
            Debug.Log("COLLIDED WITH TICKET");
            StopAllCoroutines();
            StartCoroutine(Move(ticket.transform, _pivot));
        }
    }
    private IEnumerator MoveTicket(Transform obj, Vector3 moveTo)
    {
        obj.GetComponent<Collider2D>().enabled = false;

        Vector3 posX = moveTo;
        posX.y = obj.position.y;

        StartCoroutine(Move(obj, posX));

        yield return new WaitUntil(() => obj.position.x == moveTo.x);

        Vector3 posY = moveTo;
        posY.x = obj.position.x;

        StartCoroutine(Move(obj, posY));

        yield return new WaitUntil(() => obj.position.y == moveTo.y);

        obj.GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator Move(Transform obj, Vector3 moveTo)
    {
        Vector3 startPos = obj.position;
        Vector3 endPos = moveTo;
        Vector3 newPos = startPos;

        float i = 0;
        endPos.z = obj.position.z;

        while (newPos != endPos)
        {
            if (Vector3.Distance(newPos, endPos) < 0.05)
            {
                newPos = endPos;
            }
            else
            {
                newPos = Vector3.Lerp(startPos, endPos, i);
            }

            Debug.Log("MOVING");

            obj.transform.position = newPos;

            i += _moveSpeed * Time.deltaTime;

            yield return null;
        }

        obj.GetComponent<Collider2D>().enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_pivot + (Vector3.up * _pivotDebugCrossSize), _pivot - (Vector3.up * _pivotDebugCrossSize));
        Gizmos.DrawLine(_pivot + (Vector3.right * _pivotDebugCrossSize), _pivot - (Vector3.right * _pivotDebugCrossSize));

    }
}
