using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Systems.Weapon 
{
    public class BulletsDestroyerSystem : IUpdateSystem
    {
        private readonly BulletsPool _bulletsPool;

        public BulletsDestroyerSystem(BulletsPool bulletsPool) 
        {
            _bulletsPool = bulletsPool;
        }

        public void Update()
        {           
            foreach (BulletView bullet in _bulletsPool.Bullets) 
            {
                bullet.AddToCurrentLifeTime(Time.deltaTime);

                if(CheckIfLifeTimeIsOver(bullet))                
                    DestroyBullet(bullet);             
            }

            TryToDeleteFromBulletsList();
        }

        private void TryToDeleteFromBulletsList()
        {
            if (_bulletsPool.BulletsForDelete.Count > 0)
                DeleteFromBulletsList();
        }

        private void DeleteFromBulletsList() 
        {
            foreach (var bullet in _bulletsPool.BulletsForDelete)           
                _bulletsPool.Bullets.Remove(bullet);          
        }

        private bool CheckIfLifeTimeIsOver(BulletView bullet) 
        {
            return bullet.CurrentLifeTime >= bullet.LifeTime && bullet != null;
        }

        private void DestroyBullet(BulletView bullet) 
        {
            _bulletsPool.BulletsForDelete.Add(bullet);
            bullet.Die();
        }
    }  
}