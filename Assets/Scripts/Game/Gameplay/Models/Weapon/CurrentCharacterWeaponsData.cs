using Game.Gameplay.WeaponMechanic;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class CurrentCharacterWeaponsData
    {
        public RxField<WeaponBase> CurrentWeaponViewLeft { get; set; } = new();
        public RxField<WeaponBase> CurrentWeaponViewRight { get; set; } = new();

        public readonly WeaponsCharacteristics WeaponsCharacteristics = new();
    }
}