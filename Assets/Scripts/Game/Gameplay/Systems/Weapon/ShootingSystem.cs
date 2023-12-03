using System.Collections.Generic;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Character;
using Game.Gameplay.WeaponMechanics;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon
{
    public class ShootingSystem : IUpdateSystem
    {
        private readonly CharacterWeaponsRepository _characterWeaponsRepository;
        private readonly HandIKView _leftHandIKView;
        private readonly HandIKView _rightHandIKView;

        public ShootingSystem(CharacterWeaponsRepository characterWeaponsRepository, List<HandIKView> handIKViews)
        {
            _characterWeaponsRepository = characterWeaponsRepository;

            _leftHandIKView = handIKViews.Find(x => x.HandTargetView.IsLeft);
            _rightHandIKView = handIKViews.Find(x => !x.HandTargetView.IsLeft);
        }

        public void Update()
        {
            TryToShoot(_characterWeaponsRepository.CurrentWeaponViewLeft.Value, _leftHandIKView);
            TryToShoot(_characterWeaponsRepository.CurrentWeaponViewRight.Value, _rightHandIKView);
        }

        private static void TryToShoot(WeaponBase weaponBase, HandIKView handIKView)
        {
            if (weaponBase.NeedReload())
            {
                weaponBase.ProcessReload();
                return;
            }

            weaponBase.Shoot(handIKView);
        }
    }
}