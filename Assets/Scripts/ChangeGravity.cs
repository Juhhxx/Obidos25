using System.ComponentModel;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    [Header("On Collision Enter")]
    [SerializeField] private GravityChange _changeToEnter;

    [Header("On Collision Exit")]
    [SerializeField] private GravityChange _changeToExit;

    public enum GravityChange { On, Off, None }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeGravityS(_changeToEnter, other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ChangeGravityS(_changeToExit, other.gameObject);
    }

    private void ChangeGravityS(GravityChange changeTo, GameObject other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        switch (changeTo)
        {
            case GravityChange.On:
                rb.gravityScale = 2.0f;
                break;

            case GravityChange.Off:
                rb.gravityScale = 0.0f;
                rb.linearVelocityY = 0.0f;
                break;

            default:
                return;
        }
    }
}
