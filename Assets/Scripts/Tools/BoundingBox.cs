using NaughtyAttributes;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    [OnValueChanged("UpdateRect")]
    [SerializeField] private Transform _point1;

    [OnValueChanged("UpdateRect")]
    [SerializeField] private Transform _point2;

    // Top Left Point
    private Vector3 Point1 => BoundingBoxBounds != null ?
                        BoundingBoxBounds.min :
                        Vector2.zero;
    // Bottom Right Point
    private Vector3 Point2 => BoundingBoxBounds != null ?
                        BoundingBoxBounds.max :
                        Vector2.zero;
    // Top Right Point
    private Vector2 Point3 => BoundingBoxBounds != null ?
                        new Vector2(BoundingBoxBounds.max.x, BoundingBoxBounds.min.y) :
                        Vector2.zero;
    // Bottom Left Point
    private Vector2 Point4 => BoundingBoxBounds != null ?
                        new Vector2(BoundingBoxBounds.min.x, BoundingBoxBounds.max.y) :
                        Vector2.zero;

    public Bounds BoundingBoxBounds { get; private set; }

    private void Start()
    {
        UpdateRect();
    }
    
    private void UpdateRect()
    {
        Vector3 size = Vector3.zero;
        Vector3 center = Vector3.zero;

        size.x = Mathf.Abs(_point2.position.x - _point1.position.x);
        size.y = Mathf.Abs(_point2.position.y - _point1.position.y);
        size.z = 1;

        center.x = (_point1.position.x + _point2.position.x) / 2;
        center.y = (_point1.position.y + _point2.position.y) / 2;

        BoundingBoxBounds = new Bounds(center, size);

        Debug.Log($"NEW BOUNDING RECT AT {name}:\nCenter: {center} Size: {size}");
    }

    public bool CheckIfInside(Vector2 point)
    {
        bool result = BoundingBoxBounds.Contains(point);
        Debug.Log($"Checking if {point} in inside {name} : {result}");

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(BoundingBoxBounds.min, BoundingBoxBounds.max);
        Gizmos.DrawLine(Point1, Point3);
        Gizmos.DrawLine(Point3, Point2);
        Gizmos.DrawLine(Point2, Point4);
        Gizmos.DrawLine(Point4, Point1);
    }
}
