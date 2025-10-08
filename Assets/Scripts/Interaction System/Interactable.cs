using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public event Action InteractBegin;
    public event Action Interact;
    public event Action InteractEnd;

    private void Start()
    {
        Interactable[] ints = transform.parent?.GetComponentsInParent<Interactable>();

        if (ints == null || ints.Length == 0)
        {
            transform.position += -Vector3.forward;
        }
        else Debug.Log($"{name} IS CHILD");

        Debug.Log($"{name} : {transform.position}");
    }

    public virtual void OnInteractBegin()
    {
        Debug.Log($"{name} did Interact Begin");
        InteractBegin?.Invoke();
    }
    public virtual void OnInteract()
    {
        Debug.Log($"{name} did Interact");
        Interact?.Invoke();
    }
    public virtual void OnInteractEnd()
    {
        Debug.Log($"{name} did Interact End");
        InteractEnd?.Invoke();
    }
}
