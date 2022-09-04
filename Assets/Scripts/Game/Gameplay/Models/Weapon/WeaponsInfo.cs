using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class WeaponsInfo
    {
        public List<WeaponViewBase> PlayerArsenal = new List<WeaponViewBase>();    
        public RxField<WeaponViewBase> CurrentWeaponViewLeft { get; set; } = new RxField<WeaponViewBase>();
        public RxField<WeaponViewBase> CurrentWeaponViewRight { get; set; } = new RxField<WeaponViewBase>();

        public void AddWeapon(WeaponViewBase weaponView)
        {
            PlayerArsenal.Add(weaponView);
        }

        public void RemoveWeapon(WeaponViewBase weaponView)
        {
            PlayerArsenal.Remove(weaponView);
        }
    }
}