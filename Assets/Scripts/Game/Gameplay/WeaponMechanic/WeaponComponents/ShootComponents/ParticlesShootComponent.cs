using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.FX;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents
{
    public class ParticlesShootComponent : IShootComponent
    {
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        private readonly RecyclableParticleType _particleType;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;

        public ParticlesShootComponent(RecyclableParticlesPoolFactory recyclableParticlesPoolFactory,
            WeaponsCharacteristics weaponsCharacteristics, RecyclableParticleType particleType, WeaponType weaponType,
            Transform shootingPoint)
        {
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _weaponsCharacteristics = weaponsCharacteristics;
            _particleType = particleType;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot()
        {
            if (_recyclableParticlesPoolFactory.GetElement(_particleType) is CommonShootFxView projectileView)
            {
                var particlesTransform = projectileView.transform;

                particlesTransform.position = _shootingPoint.position;
                particlesTransform.rotation = _shootingPoint.rotation;

                projectileView.Play();
                projectileView.SetEnemyCollideHandler(OnEnemyCollideHandler);

                projectileView.OnParticleSystemStopped += OnParticleSystemStoppedHandler;
                return;
            }

            Debug.LogError($"{nameof(ParticlesShootComponent)}.{nameof(Shoot)} wrong particleType: {_particleType}");
        }


        private void OnEnemyCollideHandler(EnemyView enemyView)
        {
            enemyView.TakeDamage(GetDamage(_weaponType));
        }

        private float GetDamage(WeaponType weaponType)
        {
            return _weaponsCharacteristics.GetCharacteristic(weaponType, WeaponCharacteristicType.Attack);
        }

        private void OnParticleSystemStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            recyclableParticleView.OnParticleSystemStopped -= OnParticleSystemStoppedHandler;

            _recyclableParticlesPoolFactory.RecycleElement(_particleType, recyclableParticleView);
        }
    }
}