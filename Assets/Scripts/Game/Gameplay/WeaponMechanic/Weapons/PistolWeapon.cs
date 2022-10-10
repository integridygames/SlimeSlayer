using System.Collections.Generic;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapons.Pistols;
using TegridyUtils;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class PistolWeapon : IWeapon
    {
        private readonly PistolView _pistolView;
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly ActiveBulletsContainer _activeBulletsContainer;

        private float _previousShootTime;
        private Dictionary<WeaponCharacteristicType, int> _weaponsCharacteristic;

        public PistolWeapon(PistolView pistolView, BulletsPoolFactory bulletsPoolFactory,
            CurrentCharacterWeaponsData currentCharacterWeaponsData, ActiveBulletsContainer activeBulletsContainer)
        {
            _pistolView = pistolView;
            _bulletsPoolFactory = bulletsPoolFactory;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _activeBulletsContainer = activeBulletsContainer;
        }

        public void Shoot()
        {
            if (Time.time - _previousShootTime > GetFireRate())
            {
                var bulletView = _bulletsPoolFactory.GetElement(WeaponType.Pistol);

                var shootingPointTransform = _pistolView.ShootingPoint.transform;

                bulletView.Rigidbody.position = shootingPointTransform.position;
                bulletView.Rigidbody.velocity = shootingPointTransform.forward * 5;

                _activeBulletsContainer.AddBullet(bulletView);

                _previousShootTime = Time.time;
            }
        }

        private int GetFireRate()
        {
            _weaponsCharacteristic ??= _currentCharacterWeaponsData.WeaponsCharacteristics[WeaponType.Pistol];
            return _weaponsCharacteristic[WeaponCharacteristicType.FireRate];
        }

        public bool NeedToShoot(Collider[] targets)
        {
            var transform = _pistolView.ShootingPoint.transform;

            foreach (var target in targets)
            {
                if (MathUtils.IsInCone(transform.position, transform.forward, target.transform.position, 10, 20, true))
                {
                    return true;
                }
            }

            return false;
        }
    }
}