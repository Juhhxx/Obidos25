using System.Collections.Generic;
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

        public string Regiment => "R" + Division?.Name[0] + Location?.IdRegion;

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


        private bool _isBufo = false;
        public bool IsBufo => _isBufo;

        private bool _marked = false;
        public bool IsMarked => _marked;

        private Dictionary<string, bool> _wrongAnswers = new Dictionary<string, bool>() {
            {"password", false},
            {"codename", false},
            {"location", false},
            {"parking", false},
            {"division_badge", false},
            {"rank_badge", false},
            {"sprite", false},
        };

        public Dictionary<string,bool> WrongAnswers => _wrongAnswers;

        public void Mark() => _marked = true;

        public void SetBufo()
        {
            _isBufo = true;

        }

        public Sprite GetMoleSprite()
        {
            int spriteId = Random.Range(1, Sprite.Length);
            return Sprite[spriteId];
        }

        public Military Instantiate()
        {
            return Instantiate(this);
        }

        private bool MoleChance(float chance)
        {
            float moleChance = Random.Range(0f,1f);

            return moleChance <= chance;
        }
    }
}
