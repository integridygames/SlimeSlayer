using System;
using Game.Gameplay.Models.Weapon;

namespace Game.ScriptableObjects.Weapon
{
    [Serializable]
    public class WeaponCharacteristic
    {
        public WeaponCharacteristicType _weaponCharacteristicType;

        public int _startValue;
        public float _multiplier;
    }
}