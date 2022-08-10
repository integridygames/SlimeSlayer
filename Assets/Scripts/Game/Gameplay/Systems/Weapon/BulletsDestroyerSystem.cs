using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Models.Bullets;

namespace Game.Gameplay.Systems.Weapon 
{
    public class BulletsDestroyerSystem : IUpdateSystem
    {
        private BulletsPool _bulletsPool;

        public BulletsDestroyerSystem(BulletsPool bulletsPool) 
        {
            _bulletsPool = bulletsPool;
        }

        public void Update()
        {           
            foreach (var bullet in _bulletsPool.Bullets) 
            {
                bullet.CurrentLifeTime += Time.deltaTime;
                if(bullet.CurrentLifeTime >= bullet.LifeTime && bullet != null) 
                {
                    bullet.Die();
                }
            }
        }
    }  
}