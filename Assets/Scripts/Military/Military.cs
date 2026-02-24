using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System.Linq;

namespace Obidos25
{
    [CreateAssetMenu(fileName = "Military", menuName = "Scriptable Objects/Military")]
    public class Military : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        public string GetShortName()
        {
            string[] names = Name.Split(" ");

            string shortName = names.First() + " " + names.Last();

            return shortName;
        }
        [field: SerializeField] public string CodeName { get; private set; }

        [field: Expandable]
        [field: SerializeField] public Division Division { get; private set; }

        [field: Expandable]
        [field: SerializeField] public Rank Rank { get; private set; }

        public string Regiment => "R" + Division?.Name.Replace(" ","")[0] + Location?.IdRegion;

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
            ResetWrongAnswers();
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

        const int minimumWrongAnswers = 3;
        const int maximunWrongAnswers = 6;

        private void ResetWrongAnswers()
        {
            Debug.Log($"RESET WRONG ANSWERS FOR {Name}");
            
            _wrongAnswers = new Dictionary<string, bool>() {
            {"password", false},
            {"eye_color", false},
            {"codename", false},
            {"location", false},
            {"parking", false},
            {"division_badge", false},
            {"rank_badge", false},
            {"sprite", false},
        };
        }
        public void SetWrongAnswers(bool isMole)
        {
            if (!isMole) SetWrongNotMole();
            else SetWrongMole();
        }

        private void SetWrongNotMole()
        {
            Debug.Log($"EVALUATING {Name} AS NOT MOLE");

            var tmp = _detailInfo
                        .Where(detail => !detail.OnlyMoleWrong)
                        .ToList();
            

            int wrongCounter = 0;

            foreach (DetailInfo detail in tmp)
            {
                string detailName = detail.Detail.ToLower().Replace(" ","_");

                Debug.Log($"DECIDING {detailName}");

                bool isWrong = WrongChance(detail.WrongProbabilityNotMole);

                Debug.Log($"SET {detailName} AS WRONG ? {isWrong}");

                _wrongAnswers[detailName] = isWrong;

                if (isWrong) wrongCounter++;
            }

            Debug.Log($"{Name} HAS {wrongCounter} WRONG DETAILS");

            return;       
        }

        private void SetWrongMole()
        {
            Debug.Log($"EVALUATING {Name} AS MOLE");

            int wrongAnswers = Random.Range(minimumWrongAnswers, maximunWrongAnswers + 1);

            Debug.Log($"{Name} HAS {wrongAnswers} WRONG DETAILS");

            int wrong = 0;

            while (wrong < wrongAnswers)
            {
                DetailInfo detail = _detailInfo[Random.Range(0, _detailInfo.Count)];

                string detailName = detail.Detail.ToLower().Replace(" ","_");

                Debug.Log($"DECIDING {detailName}");

                if (_wrongAnswers[detailName]) continue;

                // Try to make detail wrong

                bool isWrong = WrongChance(detail.WrongProbability);

                Debug.Log($"SET {detailName} AS WRONG ? {isWrong}");

                _wrongAnswers[detailName] = isWrong;

                if (isWrong) wrong++;
            }
        }

        private bool WrongChance(float chance)
        {
            float moleChance = Random.Range(0f,1f);

            return moleChance <= chance;
        }
    }
}
