using UnityEngine;

public class MilitaryEyeManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spr;

    private float _pixelPerUnitRatio => _spr.sprite.pixelsPerUnit;
    private Vector2 _spriteSize
    {
        get
        {
            Vector2 size = _spr.sprite.rect.size;

            // Debug.Log($"SIZE : {size}");

            return size;
        }
    }
    private int _spriteW => (int)_spriteSize.x;
    private int _spriteH => (int)_spriteSize.y;

    public void ChangeEyeColor(Color color)
    {
        Debug.Log($"READING SPRITE {_spr.sprite}");
        Texture2D tex = _spr.sprite.texture;

        // Replace White with Eye Color
        Color[] pixels = tex.GetPixels((int)_spr.sprite.rect.x, (int)_spr.sprite.rect.y, _spriteW, _spriteH);
        Color[] finalPixels = new Color[pixels.Length];

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] != Color.white)
            {
                finalPixels[i] = pixels[i];
            }
            else
            {
                finalPixels[i] = color;
            }
        }

        Texture2D finalTex = new Texture2D(_spriteW, _spriteH, TextureFormat.RGBA32, false);
        finalTex.SetPixels(finalPixels);
        finalTex.Apply();

        Vector2 spriteCenter = new Vector2(0.5f, 0.5f);
        Rect spriteRect = new Rect(0f, 0f, _spriteW, _spriteH);

        _spr.sprite = Sprite.Create(finalTex, spriteRect, spriteCenter, _pixelPerUnitRatio);
    }
}
