using System.Linq;
using UnityEngine;

namespace Obidos25
{
    [CreateAssetMenu(fileName = "Military", menuName = "Scriptable Objects/Military")]
    public class Military : ScriptableObject
    {
        public string Name;
        public string CodeName;
        public Division Division;
        public string ID;
        public float Height;
        public string Features;
        public string EyeColor;
        public string ParkingSpot;
        public Sprite Signature;
        public Sprite Picture;
        public Sprite[] Sprite;

        public Sprite GetMoleSprite()
        {
            int spriteId = Random.Range(1, Sprite.Length);
            return Sprite[spriteId];
        }
    }
}
