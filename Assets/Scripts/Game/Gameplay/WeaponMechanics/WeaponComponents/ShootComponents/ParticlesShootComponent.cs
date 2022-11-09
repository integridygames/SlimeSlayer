using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.WeaponComponents.ShootComponents
{
    public class ParticlesShootComponent : IShootComponent
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        private readonly RecyclableParticleType _particleType;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        public ParticlesShootComponent(WeaponsCharacteristics weaponsCharacteristics, RecyclableParticleType particleType, WeaponType weaponType,
            Transform shootingPoint, WeaponMechanicsService weaponMechanicsService)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _particleType = particleType;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
            _weaponMechanicsService = weaponMechanicsService;
        }

        public void Shoot()
        {
            _weaponMechanicsService.ShootFX(_particleType, _shootingPoint, OnEnemyCollideHandler);
        }

        private void OnEnemyCollideHandler(EnemyViewBase enemyViewBase)
        {
            //enemyViewBase.TakeDamage(GetDamage(_weaponType));
        }

        private float GetDamage(WeaponType weaponType)
        {
            return _weaponsCharacteristics.GetCharacteristic(weaponType, WeaponCharacteristicType.Attack);
        }
    }
}