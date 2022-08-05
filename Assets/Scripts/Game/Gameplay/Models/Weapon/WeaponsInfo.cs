using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class WeaponsInfo
    {
        public List<WeaponView> PlayerArsenal = new List<WeaponView>();    
        public RxField<WeaponView> CurrentWeaponViewLeft { get; set; } = new RxField<WeaponView>();
        public RxField<WeaponView> CurrentWeaponViewRight { get; set; } = new RxField<WeaponView>();

        public void AddWeapon(WeaponView weaponView)
        {
            PlayerArsenal.Add(weaponView);
        }

        public void RemoveWeapon(WeaponView weaponView)
        {
            PlayerArsenal.Remove(weaponView);
        }
    }
}