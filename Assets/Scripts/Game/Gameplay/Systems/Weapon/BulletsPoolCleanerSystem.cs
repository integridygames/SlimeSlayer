using TegridyCore.Base;
using Game.Gameplay.Models.Bullets;
using System.Collections.Generic;
using Game.Gameplay.Views.Bullets;

namespace Game.Gameplay.Systems.Weapon 
{
    public class BulletsPoolCleanerSystem : IUpdateSystem
    {
        public BulletsPool _bulletsPool;
        private List<BulletView> _bulletsForDelete;

        public BulletsPoolCleanerSystem(BulletsPool bulletsPool) 
        {
            _bulletsPool = bulletsPool;
            _bulletsForDelete = new List<BulletView>();
        }

        public void Update()
        {
            foreach(var bullet in _bulletsPool.Bullets) 
            {
                if (bullet == null)
                    _bulletsForDelete.Add(bullet);
            }

            foreach (var bullet in _bulletsForDelete) 
            {
                _bulletsPool.Bullets.Remove(bullet);
            }
            _bulletsForDelete.Clear();
        }
    }
}