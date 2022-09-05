using UnityEngine;
using TegridyUtils.Extensions;
using Game.Gameplay.Utils.Weapons;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : ScriptableObject
    {
        [SerializeField] private WeaponRecord[] _weapons;

        public WeaponRecord GetWeaponRecordByIndex(int index)
        {
            if (index < _weapons.Length)
            {
                return _weapons[index];
            }

            return _weapons.GetRandomElement();
        }

        public WeaponRecord GetWeaponRecordByType(WeaponType weaponType)
        {
            foreach(var weapon in _weapons) 
            {
                if(weapon._weaponType == weaponType) 
                {
                    return weapon;
                }
            }

            return _weapons.GetRandomElement();
        }
    }
}