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
        [field: SerializeField] public EyeColor EyeColor { get; private set; }
        [field: SerializeField] public ParkingSpot ParkingSpot { get; private set; }
        public void SetParking(ParkingSpot ps) => ParkingSpot = ps;
        [field: SerializeField, Expandable] public Location Location { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite Signature { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite Picture { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite[] Sprite { get; private set; }


        private bool _isMole = false;
        public bool IsMole => _isMole;
        public void SetMole()
        {
            _isMole = true;
            SetWrongAnswers(true);
        }

        private bool _marked = false;
        public bool IsMarked => _marked;

        private int _suspicionLevel = 0;
        public int SuspicionLevel => _suspicionLevel;

        public void Mark(int suspicion)
        {
            _suspicionLevel = suspicion;
            _marked = true;
        }

        [SerializeField] private List<DetailInfo> _detailInfo;

        private Dictionary<string, bool> _wrongAnswers = new Dictionary<string, bool>() {
            {"password", false},
            {"eye_color", false},
            {"codename", false},
            {"location", false},
            {"parking", false},
            {"division_badge", false},
            {"rank_badge", false},
            {"sprite", false},
        };

        public Dictionary<string,bool> WrongAnswers => _wrongAnswers;

        public Sprite GetMoleSprite()
        {
            int spriteId = Random.Range(1, Sprite.Length);
            return Sprite[spriteId];
        }

        public Military Instantiate()
        {
            Military m = Instantiate(this);

            m.SetWrongAnswers(false);

            return m;
        }

        int minimumWrongAnswers = 3;

        public void SetWrongAnswers(bool isMole)
        {
            int wrongAnswers = 0;

            foreach (DetailInfo detail in _detailInfo)
            {
                string detailName = detail.Detail.ToLower().Replace(" ","_");

                Debug.Log($"DECIDING {detailName}");

                if (_wrongAnswers[detailName]) continue;

                bool answer = false;

                if (isMole)
                {
                    answer = WrongChance(detail.WrongProbability);
                }
                else
                {
                    if (!detail.OnlyMoleWrong) answer = WrongChance(detail.WrongProbabilityNotMole);
                }

                Debug.Log($"SET {detailName} AS WRONG ? {answer}");

                _wrongAnswers[detailName] = answer;

                if (answer) wrongAnswers++;
            }

            if (isMole && wrongAnswers < minimumWrongAnswers)
            {
                var tmp = new List<string>(_wrongAnswers.Keys);

                for (int i = 0; i < minimumWrongAnswers; i++)
                {
                    int rnd = Random.Range(0, tmp.Count);

                    _wrongAnswers[tmp[rnd]] = true;

                    tmp.RemoveAt(rnd);
                }
            }
        }

        private bool WrongChance(float chance)
        {
            float moleChance = Random.Range(0f,1f);

            return moleChance <= chance;
        }
    }
}
