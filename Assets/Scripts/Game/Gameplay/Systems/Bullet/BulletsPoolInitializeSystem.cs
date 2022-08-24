using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Views.Character.Placers;
using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Bullet 
{
    public class BulletsPoolInitializeSystem : IInitializeSystem
    {
        private readonly BulletsPool _leftBulletsPool;
        private readonly BulletsPool _rightBulletsPool;
        private readonly WeaponView _leftWeaponView;
        private readonly WeaponView _rightWeaponView;

        public BulletsPoolInitializeSystem(List<BulletsPool> bulletsPools, List<WeaponPlacer> weaponPlacers) 
        {
            foreach(var pool in bulletsPools) 
            {
                if (pool.IsLeft)
                    _leftBulletsPool = pool;
                else
                    _rightBulletsPool = pool;
            }

            foreach (var placer in weaponPlacers)
            {
                if (placer.IsLeft)
                    _leftWeaponView = placer.GetComponentInChildren<WeaponView>();
                else
                    _rightWeaponView = placer.GetComponentInChildren<WeaponView>();
            }
        }

        public void Initialize()
        {
            _leftBulletsPool.CreatePool(_leftWeaponView.BulletTemplate);
            _rightBulletsPool.CreatePool(_rightWeaponView.BulletTemplate);
        }
    }
}