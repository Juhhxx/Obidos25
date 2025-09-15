using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Obidos25
{
    [CreateAssetMenu(fileName = "Military", menuName = "Scriptable Objects/Military")]
    public class Military : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string CodeName { get; private set; }

        [field: Expandable]
        [field: SerializeField] public Division Division { get; private set; }

        [field: Expandable]
        [field: SerializeField] public Rank Rank { get; private set; }

        public string Regiment => "R" + Division?.DivisionName[0] + Location?.IdRegion;

        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public float Height { get; private set; }
        [field: SerializeField] public string Features { get; private set; }
        [field: SerializeField] public string EyeColor { get; private set; }
        [field: SerializeField] public ParkingSpot ParkingSpot { get; private set; }
        public void SetParking(ParkingSpot ps) => ParkingSpot = ps;
        [field: SerializeField] public Location Location { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite Signature { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite Picture { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite[] Sprite { get; private set; }

        private bool _marked = false;
        public bool IsMarked => _marked;

        public void Mark() => _marked = true;

        public Sprite GetMoleSprite()
        {
            int spriteId = Random.Range(1, Sprite.Length);
            return Sprite[spriteId];
        }

        public Military Instantiate()
        {
            return Instantiate(this);
        }
    }
}
