using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Abilities;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Abilities
{
    [Serializable]
    public class AbilityRecord
    {
        [SerializeField, ArrayKey] private AbilityType _abilityType;
        [SerializeField] private Sprite _abilitySprite;

        [ArrayWithKey] [SerializeField] private List<AbilityLevelRecord> _levelInfos = new();
        [SerializeField] private List<WeaponType> _weaponNeeds;
        [SerializeField] private List<AbilityType> _abilityNeeds;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        private HashSet<WeaponType> _weaponNeedsSet;
        private HashSet<AbilityType> _abilityNeedsSet;

        public int MaxLevel => _levelInfos.Count;

        public AbilityType AbilityType => _abilityType;

        public Sprite AbilitySprite => _abilitySprite;


        public HashSet<WeaponType> WeaponNeedsSet
        {
            get { return _weaponNeedsSet ??= _weaponNeeds.ToHashSet(); }
        }

        public HashSet<AbilityType> AbilityNeedsSet
        {
            get { return _abilityNeedsSet ??= _abilityNeeds.ToHashSet(); }
        }

        public string Description => _description;

        public string Name => _name;

        private Dictionary<int, AbilityLevelRecord> _abilityValues;

        public AbilityLevelRecord GetInfoForLevel(int level)
        {
            if (_abilityValues == null)
            {
                FillAbilitiesCache();
            }

            return _abilityValues[level];
        }

        private void FillAbilitiesCache()
        {
            _abilityValues = new Dictionary<int, AbilityLevelRecord>();

            foreach (var levelInfo in _levelInfos)
            {
                _abilityValues[levelInfo._level] = levelInfo;
            }
        }

        [Serializable]
        public class AbilityLevelRecord
        {
            [ArrayKey] public int _level;
            public string _wholeEffect;

            public List<AbilityCharacteristicLevelInfo> _abilityCharacteristics;
        }

        [Serializable]
        public class AbilityCharacteristicLevelInfo
        {
            [ArrayKey] public AbilityCharacteristicType _abilityCharacteristicType;
            public float _value;
        }
    }
}