using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public UnityEvent OnPointerEnterEvent;
    public UnityEvent OnPointerExitEvent;
    public UnityEvent OnPointerDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke();
    }
}
