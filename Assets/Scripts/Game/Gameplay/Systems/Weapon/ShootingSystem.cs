using Game.Gameplay.Models.Weapon;
using Game.Gameplay.WeaponMechanics;
using TegridyCore.Base;

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

        private static void TryToShoot(WeaponBase weaponBase)
        {
            if (weaponBase.NeedReload())
            {
                weaponBase.ProcessReload();
                return;
            }

            weaponBase.Shoot();
        }
    }
}