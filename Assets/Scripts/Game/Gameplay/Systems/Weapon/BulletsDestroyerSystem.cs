using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Views.Bullets;
using System.Collections.Generic;

namespace Game.Gameplay.Systems.Weapon 
{
    public class BulletsDestroyerSystem : IUpdateSystem
    {
        private readonly BulletsPool _leftBulletsPool;
        private readonly BulletsPool _rightBulletsPool;

        public BulletsDestroyerSystem(List<BulletsPool> bulletsPools) 
        {
            foreach (var pool in bulletsPools)
            {
                if (pool.IsLeft)
                    _leftBulletsPool = pool;
                else
                    _rightBulletsPool = pool;
            }
        }

        public void Update()
        {
            CalculateLifeTime(Time.deltaTime, _leftBulletsPool);
            CalculateLifeTime(Time.deltaTime, _rightBulletsPool);
        }

        private void CalculateLifeTime(float deltaTime, BulletsPool pool) 
        {
            foreach (BulletView bullet in pool.Bullets)
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
            bullet.Die();
        }
    }  
}