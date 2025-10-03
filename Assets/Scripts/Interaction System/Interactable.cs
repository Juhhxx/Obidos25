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
        Interactable[] ints = GetComponents<Interactable>();

        if (ints.Length > 1)
        {
            foreach (Interactable i in ints)
            {
                if (i == this) continue;

                if (!i.didStart) transform.position += -Vector3.forward;
            }
        }
        else
        {
            transform.position += -Vector3.forward;
        }
        
        Debug.Log($"{name} : {transform.position}");
    }

    public virtual void OnInteractBegin() => InteractBegin?.Invoke();
    public virtual void OnInteract() => Interact?.Invoke();
    public virtual void OnInteractEnd() => InteractEnd?.Invoke();
}
