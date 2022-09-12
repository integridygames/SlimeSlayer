using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;

namespace Game.Gameplay.Systems.Weapon 
{
    public class BulletsDestroyerSystem : IUpdateSystem
    {
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly ActiveBulletsContainer _activeBulletsContainer;

        public BulletsDestroyerSystem(BulletsPoolFactory bulletsPoolFactory, ActiveBulletsContainer activeBulletsContainer)
        {
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeBulletsContainer = activeBulletsContainer;

        }

        public void Update()
        {
            CalculateLifeTime(Time.deltaTime);
        }

        private void CalculateLifeTime(float deltaTime) 
        {
            foreach (var bullet in _activeBulletsContainer.ActiveBullets)
            {
                bullet.AddToCurrentLifeTime(deltaTime);

                if (CheckIfLifeTimeIsOver(bullet))
                    DestroyBullet(bullet);
            }
        }

        private bool CheckIfLifeTimeIsOver(BulletView bullet) 
        {
            return bullet.CurrentLifeTime >= bullet.LifeTime && bullet != null;
        }

        private void DestroyBullet(BulletView bullet) 
        {
            bullet.Rigidbody.velocity = Vector3.zero;
            _bulletsPoolFactory.RecycleElement(bullet, bullet.WeaponType);
        }
    }  
}