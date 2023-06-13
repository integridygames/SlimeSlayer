using System;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class PlayerWeaponData
    {
        public string _guid;
        public WeaponType _weaponType;
        public RarityType _rarityType;
        public int _level;

        public PlayerWeaponData(WeaponType weaponType, RarityType rarityType)
        {
            _weaponType = weaponType;
            _rarityType = rarityType;
            _guid = Guid.NewGuid().ToString();
        }
    }
}