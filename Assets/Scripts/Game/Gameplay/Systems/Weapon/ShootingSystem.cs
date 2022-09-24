using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.WeaponMechanic;

namespace Game.Gameplay.Systems.Weapon
{
    public class ShootingSystem : IUpdateSystem
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly TargetsInfo _targetsInfo;

        public ShootingSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData,
            TargetsInfo targetsInfo)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _targetsInfo = targetsInfo;
        }

        public void Update()
        {
            if (CheckShootingNecessity())
            {
                TryToShoot(_currentCharacterWeaponsData.CurrentWeaponViewLeft.Value);
                TryToShoot(_currentCharacterWeaponsData.CurrentWeaponViewRight.Value);
            }
        }

        private bool CheckShootingNecessity()
        {
            return _targetsInfo.Targets.Length > 0;
        }

        private static void TryToShoot(IWeapon weaponView)
        {
            if (weaponView.IsOnReload())
            {
                weaponView.Shoot();
            }
        }
    }
}