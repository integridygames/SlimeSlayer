using Game.Gameplay.Models.Weapon;
using TegridyCore.Base;
using Game.Gameplay.WeaponMechanic;

namespace Game.Gameplay.Systems.Weapon
{
    public class ShootingSystem : IUpdateSystem
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;

        public ShootingSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
        }

        public void Update()
        {
            TryToShoot(_currentCharacterWeaponsData.CurrentWeaponViewLeft.Value);
            TryToShoot(_currentCharacterWeaponsData.CurrentWeaponViewRight.Value);
        }

        private static void TryToShoot(IWeapon weapon)
        {
            if (weapon.NeedToShoot())
            {
                weapon.Shoot();
            }
        }
    }
}