using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class Button : Interactable
{
    public UnityEvent OnButtonClickDown;
    public UnityEvent OnButtonClickUp;

    public SpriteShapeRenderer HighlightSR { get; private set; }

    private void Start()
    {
        InteractBegin += OnButtonClickDown.Invoke;
        InteractEnd += OnButtonClickUp.Invoke;

        Draggabble drag = GetComponentInParent<Draggabble>();

        if (drag != null)
        {
            InteractBegin   += drag.OnInteractBegin;
            Interact        += drag.OnInteract;
            InteractEnd     += drag.OnInteractEnd;
        }

        HighlightSR = GetComponent<SpriteShapeRenderer>();

        if (HighlightSR != null) HighlightSR.enabled = false;

        OnButtonClickDown.AddListener(() => Debug.Log($"BUTTON {name} PRESSED"));
        OnButtonClickUp.AddListener(() => Debug.Log($"BUTTON {name} RELEASED"));
    }
    
}
