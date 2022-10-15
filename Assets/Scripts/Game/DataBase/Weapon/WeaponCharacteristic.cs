using System;
using Game.Gameplay.Models.Weapon;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponCharacteristic
    {
        public WeaponCharacteristicType _weaponCharacteristicType;

        public float _startValue;
        public float _multiplier;
    }
}