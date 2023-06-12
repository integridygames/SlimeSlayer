using System;
using Game.Gameplay.Models.Weapon;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponCharacteristicData
    {
        public WeaponCharacteristicType _weaponCharacteristicType;

        public float _startValue;

        public float _addition;
        public float _additionMultiplier;
    }
}