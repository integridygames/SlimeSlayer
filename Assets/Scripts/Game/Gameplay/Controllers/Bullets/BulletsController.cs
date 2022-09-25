using System;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Bullets
{
    public class BulletsController : ControllerBase<ActiveBulletsContainer>, IInitializable, IDisposable
    {
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;

        public BulletsController(ActiveBulletsContainer controlledEntity, BulletsPoolFactory bulletsPoolFactory,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(controlledEntity)
        {
            _bulletsPoolFactory = bulletsPoolFactory;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
        }

        public void Initialize()
        {
            ControlledEntity.OnBulletCollide += OnBulletCollideHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnBulletCollide -= OnBulletCollideHandler;
        }

        private void OnBulletCollideHandler(EnemyView enemyView, BulletView bulletView)
        {
            ControlledEntity.RemoveBullet(bulletView);
            _bulletsPoolFactory.RecycleElement(bulletView);

            enemyView.TakeDamage(GetDamage());
        }

        private float GetDamage()
        {
            var weaponsCharacteristics = _currentCharacterWeaponsData.WeaponsCharacteristics[WeaponType.Pistol];
            return weaponsCharacteristics[WeaponCharacteristicType.Attack];
        }
    }
}