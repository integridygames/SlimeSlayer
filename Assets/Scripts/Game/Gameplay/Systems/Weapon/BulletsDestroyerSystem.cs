using System.Collections.Generic;
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

        private readonly List<BulletView> _bulletsForDestroy = new();

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
            foreach (var bulletView in _activeBulletsContainer.ActiveBullets)
            {
                bulletView.AddToCurrentLifeTime(deltaTime);

                if (CheckIfLifeTimeIsOver(bulletView))
                    _bulletsForDestroy.Add(bulletView);
            }

            foreach (var bulletView in _bulletsForDestroy)
            {
                DestroyBullet(bulletView);
            }

            _bulletsForDestroy.Clear();
        }

        private bool CheckIfLifeTimeIsOver(BulletView bullet) 
        {
            return bullet.CurrentLifeTime >= bullet.LifeTime && bullet != null;
        }

        private void DestroyBullet(BulletView bullet) 
        {
            bullet.Rigidbody.velocity = Vector3.zero;
            _activeBulletsContainer.RemoveBullet(bullet);
            _bulletsPoolFactory.RecycleElement(bullet);
        }
    }  
}