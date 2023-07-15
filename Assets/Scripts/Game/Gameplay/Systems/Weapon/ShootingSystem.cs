using Game.Gameplay.Models.Weapon;
using Game.Gameplay.WeaponMechanics;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon
{
    public class ShootingSystem : IUpdateSystem
    {
        private readonly CharacterWeaponsRepository _characterWeaponsRepository;

        public ShootingSystem(CharacterWeaponsRepository characterWeaponsRepository)
        {
            _characterWeaponsRepository = characterWeaponsRepository;
        }

        public void Update()
        {
            TryToShoot(_characterWeaponsRepository.CurrentWeaponViewLeft.Value);
            TryToShoot(_characterWeaponsRepository.CurrentWeaponViewRight.Value);
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