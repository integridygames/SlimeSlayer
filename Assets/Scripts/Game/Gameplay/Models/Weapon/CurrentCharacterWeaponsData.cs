using Game.Gameplay.WeaponMechanics;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class CurrentCharacterWeaponsData
    {
        public RxField<WeaponBase> CurrentWeaponViewLeft { get; set; } = new();
        public RxField<WeaponBase> CurrentWeaponViewRight { get; set; } = new();
    }
}