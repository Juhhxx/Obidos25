using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public UnityEvent OnButtonClickDown;
    public UnityEvent OnButtonClickUp;

    private void OnEnable()
    {
        InteractBegin += OnButtonClickDown.Invoke;
        InteractEnd += OnButtonClickUp.Invoke;
        
        OnButtonClickDown.AddListener(() => Debug.Log($"BUTTON {name} PRESSED"));
        OnButtonClickUp.AddListener(() => Debug.Log($"BUTTON {name} RELEASED"));
    }
    private void OnDisable()
    {
        InteractBegin -= OnButtonClickDown.Invoke;
        InteractEnd -= OnButtonClickUp.Invoke;
    }
}
