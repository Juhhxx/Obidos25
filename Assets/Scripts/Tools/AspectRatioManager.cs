using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class AspectRatioManager : MonoBehaviour
{
    [SerializeField] private float _targetAspectRatio = 16f / 9f; // The desired aspect ratio, e.g., 16:9
    private Camera _camera;
   

    private void Start()
    {
        _camera = GetComponent<Camera>();
        SetCameraAspect();
    }

    private void SetCameraAspect()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / _targetAspectRatio;

        if (scaleHeight < 1.0f)
        {
            // Letterboxing
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            _camera.rect = rect;
        }
        else
        {
            // Pillarboxing
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = _camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}