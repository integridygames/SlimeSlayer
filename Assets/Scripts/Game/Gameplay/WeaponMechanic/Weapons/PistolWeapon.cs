using System.Collections.Generic;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons.Pistols;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class PistolWeapon : IWeapon
    {
        private const int BulletSpeed = 15;

        private readonly PistolView _pistolView;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;

        private float _previousShootTime;

        private Dictionary<WeaponCharacteristicType, int> _weaponsCharacteristic;

        public PistolWeapon(PistolView pistolView, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            _pistolView = pistolView;
            _weaponMechanicsService = weaponMechanicsService;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
        }

        public void Shoot()
        {
            if (Time.time - _previousShootTime > GetFireRate())
            {
                _weaponMechanicsService.ShootABullet(_pistolView.ShootingPoint, WeaponType.Pistol, BulletSpeed);

                _previousShootTime = Time.time;
            }
        }

        private int GetFireRate()
        {
            _weaponsCharacteristic ??= _currentCharacterWeaponsData.WeaponsCharacteristics[WeaponType.Pistol];
            return _weaponsCharacteristic[WeaponCharacteristicType.FireRate];
        }

        public bool NeedToShoot()
        {
            return _weaponMechanicsService.WeaponHasATarget(_pistolView.ShootingPoint);
        }
    }
}