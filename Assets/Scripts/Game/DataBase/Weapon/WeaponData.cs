using System;
using TegridyUtils.Attributes;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponData
    {
        [ArrayKey]
        public WeaponType _weaponType;
        public RarityType _rarityType;
        public int _level;
    }
}