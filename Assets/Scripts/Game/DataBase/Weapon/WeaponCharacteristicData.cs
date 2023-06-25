using System;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponCharacteristicData
    {
        [ArrayKey]
        public WeaponCharacteristicType _weaponCharacteristicType;

        public float _startValue;

        public float _addition;
        public float _additionMultiplier;

        [Tooltip("Hidden from player. For technical usage.")]
        public bool _hidden;

        [Tooltip("Used like percentage value.")]
        public bool _isPercentage;
    }
}