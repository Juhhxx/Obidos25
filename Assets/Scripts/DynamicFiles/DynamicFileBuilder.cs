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

    [SerializeField] private bool _captureToRenderer;
    [SerializeField][ShowIf("_captureToRenderer")] private SpriteRenderer _applyToRenderer;

    [SerializeField][HideIf("_captureToRenderer")] private BookPageManager _applyToBook;
    [SerializeField][HideIf("_captureToRenderer")] private int _bookPage;

    [SerializeField] private TextureFormat _textureFormat;
    [ShowAssetPreview]
    [SerializeField] private Texture2D _generatedTexture;

    private static bool _isTakingPhoto = false;


    [Button(enabledMode: EButtonEnableMode.Always)]
    public void BuildFileSprite()
    {
        Debug.Log($"{_captureCamera.aspect}");

        float height = 2f * _captureCamera.orthographicSize;
        float width = height * _captureCamera.aspect;
        Debug.Log($"Width : {width} Height : {height}");
        Debug.Log($"Width : {Screen.width} Height : {Screen.height}");

        // StopAllCoroutines();
        StartCoroutine(BuildFileSpriteCR());
    }

    private IEnumerator BuildFileSpriteCR()
    {
        if (_isTakingPhoto)
            Debug.Log($"{name} : WAITING FOR ANOTHER SCREENSHOT", this);

        yield return new WaitUntil(() => !_isTakingPhoto);
        
        _isTakingPhoto = true;
        Debug.Log($"{name} : BEGIN SCREENSHOT", this);

        ChangeGOLayer(_captureFrom, _captureLayer);

        yield return null;
        yield return new WaitForEndOfFrame();

        _captureCamera.Render();

        Sprite fileSprite = ToSprite(_captureTexture);

        if (_captureToRenderer) _applyToRenderer.sprite = fileSprite;
        else _applyToBook.SetPageSprite(_bookPage, fileSprite);

        yield return new WaitForEndOfFrame();

        ChangeGOLayer(_captureFrom, 0);

        yield return null;
        yield return new WaitForEndOfFrame();

        Debug.Log($"{name} : END SCREENSHOT", this);
        _isTakingPhoto = false;
    }

    private void ChangeGOLayer(GameObject go, int layer)
    {
        go.layer = layer;

        if (go.transform.childCount > 0)
        {
            Transform[] children = go.GetComponentsInChildren<Transform>();

            foreach (Transform cgo in children)
            {
                cgo.gameObject.layer = layer;
            }
        }
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

        return Sprite.Create(finalTex, spriteRect, spriteCenter, 30);
    }

}