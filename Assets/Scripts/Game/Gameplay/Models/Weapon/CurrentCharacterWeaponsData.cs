using System.Collections.Generic;
using Game.Gameplay.WeaponMechanic;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class CurrentCharacterWeaponsData
    {
        public RxField<IWeapon> CurrentWeaponViewLeft { get; set; } = new();
        public RxField<IWeapon> CurrentWeaponViewRight { get; set; } = new();

        public Dictionary<WeaponType, Dictionary<WeaponCharacteristicType, int>> WeaponsCharacteristics = new();
    }
}