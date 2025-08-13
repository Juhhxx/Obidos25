using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTrans;
    private int _childCount;

    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        _childCount = _canvas.transform.childCount;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTrans.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        transform.SetSiblingIndex(_childCount);
    }
}
