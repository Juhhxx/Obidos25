using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public UnityEvent OnPointerEnterEvent;
    public UnityEvent OnPointerDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke();
    }
}
