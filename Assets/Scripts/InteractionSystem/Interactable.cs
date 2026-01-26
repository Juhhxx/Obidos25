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
        else Debug.Log($"{name} IS CHILD", this);

        Debug.Log($"{name} : {transform.position}");
    }

    public virtual void OnInteractBegin()
    {
        Debug.LogWarning($"{name} did Interact Begin", this);
        InteractBegin?.Invoke();
    }
    public virtual void OnInteract()
    {
        Debug.LogWarning($"{name} did Interact", this);
        Interact?.Invoke();
    }
    public virtual void OnInteractEnd()
    {
        Debug.LogWarning($"{name} did Interact End", this);
        InteractEnd?.Invoke();
    }
}
