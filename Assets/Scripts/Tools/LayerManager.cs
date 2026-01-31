using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.U2D;

public class LayerManager : MonoBehaviourSingleton<LayerManager>
{
    [SerializeField][ReadOnly] private List<Draggabble> _draggabbles = new List<Draggabble>();
    [SerializeField][ReadOnly] private Draggabble _lastSelectedItem;

    private void Awake()
    {
        base.SingletonCheck(this, false);
    }

    public void RegisterDragable(Draggabble drag)
    {
        _draggabbles.Insert(0, drag);

        drag.InteractBegin += () => UpdateSelected(drag);
    }

    public void UnregisterDragable(Draggabble drag)
    {
        _draggabbles.Remove(drag);

        drag.InteractBegin -= () => UpdateSelected(drag);

        UpdateLayering();

        if (drag == _lastSelectedItem) _lastSelectedItem = null;
    }

    private void OnDisable()
    {
        foreach (var drag in _draggabbles)
        {
            drag.InteractBegin -= () => UpdateSelected(drag);
        }
    }

    private void UpdateLayering()
    {
        int k = 0;

        for (int i = 0; i < _draggabbles.Count; i++)
        {

            Draggabble drag = _draggabbles[i];

            UpdateRendererOrderInLayer(drag.gameObject, k);

            Vector3 pos = drag.transform.position;
            pos.z = (-1f - (0.1f * i));

            drag.transform.position = pos;

            k += 2;
        }
    }

    public void UpdateRendererLayer(GameObject obj, string layer)
    {
        SpriteRenderer[] aditionalRenderers = obj.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);

        if (aditionalRenderers != null || aditionalRenderers.Length != 0)
        {
            foreach (SpriteRenderer r in aditionalRenderers)
            {
                r.sortingLayerName = layer;
            }
        }

        Canvas canvas = obj.GetComponentInChildren<Canvas>(includeInactive: true);
        if (canvas != null)
        {
            canvas.sortingLayerName = layer;
        }
    }

    public void UpdateRendererOrderInLayer(GameObject obj, int order)
    {
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
        SpriteShapeRenderer[] shapeRenderers = obj.GetComponentsInChildren<SpriteShapeRenderer>(includeInactive: true);

        if (renderers != null || renderers.Length != 0)
        {
            foreach (SpriteRenderer r in renderers)
            {
                r.sortingOrder = order;

                Debug.LogWarning($"Move {obj.name} to {order}");
            }

            foreach (SpriteShapeRenderer sr in shapeRenderers)
            {
                sr.sortingOrder = order + 1;
            }
        }

        Canvas canvas = obj.GetComponentInChildren<Canvas>(includeInactive: true);

        if (canvas != null)
        {
            canvas.sortingOrder = order;
        }
    }

    public void UpdateSelected(Draggabble obj)
    {
        // If Selected Object Didn't Change, Ignore Call
        if (_lastSelectedItem == this) return;

        Debug.Log("UPDATE SELECTED");

        _lastSelectedItem = obj;

        // Move Selected Object to Last Position
        _draggabbles.Remove(obj);
        _draggabbles.Add(obj);

        UpdateLayering();
    }
}
