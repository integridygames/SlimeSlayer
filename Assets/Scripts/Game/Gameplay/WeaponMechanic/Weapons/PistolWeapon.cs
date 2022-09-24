using System.Collections.Generic;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapons.Pistols;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class PistolWeapon : IWeapon
    {
        private readonly PistolView _pistolView;
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly ActiveBulletsContainer _activeBulletsContainer;
        private readonly Dictionary<WeaponCharacteristicType, int> _pistolCharacteristics;

        private float _previousShootTime;

        public PistolWeapon(PistolView pistolView, BulletsPoolFactory bulletsPoolFactory,
            CurrentCharacterWeaponsData currentCharacterWeaponsData, ActiveBulletsContainer activeBulletsContainer)
        {
            _pistolView = pistolView;
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeBulletsContainer = activeBulletsContainer;
            _pistolCharacteristics = currentCharacterWeaponsData.WeaponsCharacteristics[WeaponType.Pistol];
        }

        public void Shoot()
        {
            if (Time.time - _previousShootTime > GetFireRate())
            {
                var bulletView = _bulletsPoolFactory.GetElement(WeaponType.Pistol);

                _activeBulletsContainer.AddBullet(bulletView);

                _pistolView.Shoot(bulletView);
            }
        }

        private int GetFireRate()
        {
            return _pistolCharacteristics[WeaponCharacteristicType.FireRate];
        }

        public bool IsOnReload()
        {
            return false;
        }
    }
}