using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class DynamicFileBuilder : MonoBehaviour
{
    [SerializeField] private Camera _captureCamera;

    [ShowAssetPreview]
    [SerializeField] private RenderTexture _captureTexture;

    [Layer]
    [SerializeField] private int _captureLayer;

    [ShowAssetPreview]
    [SerializeField] private Sprite _defaultSprite;

    private float _pixelPerUnitRatio => _defaultSprite.pixelsPerUnit;
    private Vector2 _spriteSize
    {
        get
        {
            Vector2 size = _captureFrom.GetComponent<SpriteRenderer>().bounds.size * _pixelPerUnitRatio;

            // Debug.Log($"SIZE : {size}");

            return size;
        }
    }
    private int _spriteW => (int)_spriteSize.x;
    private int _spriteH => (int)_spriteSize.y;

    [SerializeField] private GameObject _captureFrom;
    [SerializeField] private SpriteRenderer _applyTo;

    [SerializeField] private TextureFormat _textureFormat;
    [ShowAssetPreview]
    [SerializeField] private Texture2D _generatedTexture;

    private bool _hasChangedLayers = false;
    private bool _hasGeneratedSprite = false;


    [Button(enabledMode: EButtonEnableMode.Always)]
    public void BuildFileSprite()
    {
        Debug.Log($"{_captureCamera.aspect}");

        float height = 2f * _captureCamera.orthographicSize;
        float width = height * _captureCamera.aspect;
        Debug.Log($"Width : {width} Height : {height}");
        Debug.Log($"Width : {Screen.width} Height : {Screen.height}");

        StopAllCoroutines();
        StartCoroutine(BuildFileSpriteCR());
    }

    private IEnumerator BuildFileSpriteCR()
    {
        Debug.Log("BEGIN SCREENSHOT");

        _hasChangedLayers = false;
        _hasGeneratedSprite = false;

        ChangeGOLayer(_captureFrom, _captureLayer);

        yield return new WaitForEndOfFrame();

        Sprite fileSprite = ToSprite(_captureTexture);

        _applyTo.sprite = fileSprite;

        yield return new WaitForEndOfFrame();

        ChangeGOLayer(_captureFrom, 0);

        Debug.Log("END SCREENSHOT");
    }

    private void ChangeGOLayer(GameObject go, int layer)
    {
        go.layer = layer;

        if (go.transform.childCount > 0)
        {
            Transform[] children = GetComponentsInChildren<Transform>();

            foreach (Transform cgo in children)
            {
                cgo.gameObject.layer = layer;
            }
        }
        _hasChangedLayers = true;
    }

    private Sprite ToSprite(RenderTexture renderTex)
    {
        Debug.Log("SCEENSHOT TAKEN");

        Texture2D tex = new Texture2D(_spriteW, _spriteH, TextureFormat.RGB24, false, true);
        RenderTexture.active = renderTex;

        Vector2 center = new Vector2(renderTex.width / 2, renderTex.height / 2);

        Rect rect = new Rect(center.x - (_spriteW/2), center.y - (_spriteH/2), _spriteW, _spriteH);

        tex.ReadPixels(rect, 0, 0);
        tex.Apply();

        // Remove Green Background
        Color[] pixels = tex.GetPixels();
        Color[] finalPixels = new Color[pixels.Length];

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] != Color.green) 
                finalPixels[i] = pixels[i];
            else
                finalPixels[i] = new Color(0, 0, 0, 0);
        }

        Texture2D finalTex = new Texture2D(_spriteW, _spriteH, _textureFormat, false);
        finalTex.SetPixels(finalPixels);
        finalTex.Apply();

        _generatedTexture = finalTex;

        // Create Sprite Based on Texture
        Vector2 spriteCenter = new Vector2(0.5f, 0.5f);
        Rect spriteRect = new Rect(0f, 0f, _spriteW, _spriteH);

        _hasGeneratedSprite = true;

        return Sprite.Create(finalTex, spriteRect, spriteCenter, 30);
    }

}