using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents
{
    public class BulletShootComponent : IShootComponent
    {
        private const int BulletSpeed = 15;

        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        private readonly BulletType _bulletType;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;

        public BulletShootComponent(WeaponMechanicsService weaponMechanicsService,
            WeaponsCharacteristics weaponsCharacteristics, BulletType bulletType,
            WeaponType weaponType, Transform shootingPoint)
        {
            _weaponMechanicsService = weaponMechanicsService;
            _weaponsCharacteristics = weaponsCharacteristics;
            _bulletType = bulletType;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot()
        {
            _weaponMechanicsService.ShootABullet(_shootingPoint, _bulletType, BulletSpeed, OnEnemyCollideHandler);
        }

        private void OnEnemyCollideHandler(EnemyView enemyView, BulletView bulletView)
        {
            enemyView.TakeDamage(GetDamage(_weaponType));
            _weaponMechanicsService.RecycleBullet(bulletView);
        }

        private float GetDamage(WeaponType weaponType)
        {
            return _weaponsCharacteristics.GetCharacteristic(weaponType, WeaponCharacteristicType.Attack);
        }
    }
}