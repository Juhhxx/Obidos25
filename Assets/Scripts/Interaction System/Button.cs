using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    public UnityEvent OnButtonClick;

    private void OnEnable()
    {
        InteractBegin += OnButtonClick.Invoke;
        OnButtonClick.AddListener(() => Debug.Log($"BUTTON {name} PRESSED"));
    }
    private void OnDisable()
    {
        InteractBegin -= OnButtonClick.Invoke;
    }
}
