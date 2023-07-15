using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Abilities;
using TegridyUtils.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DataBase.Abilities
{
    [Serializable]
    public class AbilityRecord
    {
        [ArrayKey] [SerializeField] private AbilityType _abilityType;
        [SerializeField] private Image _abilitySprite;

        [ArrayWithKey] [SerializeField] private List<AbilityLevelRecord> _levelInfos = new();
        [SerializeField] private List<WeaponType> _weaponNeeds;
        [SerializeField] private List<AbilityType> _abilityNeeds;

        private HashSet<WeaponType> _weaponNeedsSet;
        private HashSet<AbilityType> _abilityNeedsSet;

        public int MaxLevel => _levelInfos.Count;

        public AbilityType AbilityType => _abilityType;

        public Image AbilitySprite => _abilitySprite;


        public HashSet<WeaponType> WeaponNeedsSet
        {
            get { return _weaponNeedsSet ??= _weaponNeeds.ToHashSet(); }
        }

        public HashSet<AbilityType> AbilityNeedsSet
        {
            get { return _abilityNeedsSet ??= _abilityNeeds.ToHashSet(); }
        }

        private Dictionary<(AbilityCharacteristicType, int), float> _abilityValues;

        public float GetValueForLevel(int level, AbilityCharacteristicType abilityCharacteristicType)
        {
            if (_abilityValues == null)
            {
                FillAbilitiesCache();
            }

            return _abilityValues[(abilityCharacteristicType, level)];
        }

        private void FillAbilitiesCache()
        {
            _abilityValues = new Dictionary<(AbilityCharacteristicType, int), float>();

            foreach (var levelInfo in _levelInfos)
            {
                foreach (var abilityCharacteristic in levelInfo._abilityCharacteristics)
                {
                    _abilityValues[(abilityCharacteristic._abilityCharacteristicType, levelInfo._level)] =
                        abilityCharacteristic._value;
                }
            }
        }

        [Serializable]
        private class AbilityLevelRecord
        {
            [ArrayKey] public int _level;

            public List<AbilityCharacteristicLevelInfo> _abilityCharacteristics;
        }

        [Serializable]
        private class AbilityCharacteristicLevelInfo
        {
            [ArrayKey] public AbilityCharacteristicType _abilityCharacteristicType;
            public float _value;
        }
    }
}