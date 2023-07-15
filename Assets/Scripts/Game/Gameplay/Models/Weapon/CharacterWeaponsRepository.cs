using Game.Gameplay.WeaponMechanics;
using TegridyCore;

namespace Game.Gameplay.Models.Weapon 
{
    public class CharacterWeaponsRepository
    {
        public RxField<WeaponBase> CurrentWeaponViewLeft { get; set; } = new();
        public RxField<WeaponBase> CurrentWeaponViewRight { get; set; } = new();
    }
}