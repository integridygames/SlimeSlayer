using Game.Gameplay.Views.Weapons;
using UnityEngine;
using TegridyUtils.Extensions;
using Game.Gameplay.Utils.Weapons;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : ScriptableObject
    {
        [SerializeField] private WeaponView[] _weapons;

        public WeaponView GetWeaponPrefabByIndex(int index)
        {
            if (index < _weapons.Length)
            {
                return _weapons[index];
            }

            return _weapons.GetRandomElement();
        }

        public WeaponView GetWeaponPrefabByID(WeaponsEnum ID)
        {
            foreach(var weapon in _weapons) 
            {
                if(weapon.ID == ID) 
                {
                    return weapon;
                }
            }

            return _weapons.GetRandomElement();
        }
    }
}