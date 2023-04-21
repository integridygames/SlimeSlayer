using System;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType _weaponType;
        public RarityType _rarityType;
        public int _level;
        public int _upgradePrice;
    }
}