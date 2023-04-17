using System;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public struct WeaponSaveData
    {
        public WeaponType _weaponType;
        public RarityType _rarityType;
        public int _level;
    }
}