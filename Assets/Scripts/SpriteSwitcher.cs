using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;

    public void SwitchSprite() => _spriteRenderer.sprite = _sprite;
    public void SwitchSprite(Sprite sprite) => _spriteRenderer.sprite = _sprite;
}
