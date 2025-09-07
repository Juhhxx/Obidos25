using UnityEngine;

namespace Obidos25
{
    public static class BoxCollider2DExtensions
    {
        public static void UpdateColliderBasedOnSprite(this BoxCollider2D collider, Sprite sprite)
        {
            if (sprite == null) return;

            Vector2 newSize = sprite.bounds.size;
            Vector2 newOffset = sprite.bounds.center;

            collider.size = newSize;
            collider.offset = newOffset;
        }
    }
}
