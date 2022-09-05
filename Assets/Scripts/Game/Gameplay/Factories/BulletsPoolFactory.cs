using System.Collections.Generic;
using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory
    {
        private readonly BulletsContainerView _bulletsContainerView;
        private readonly WeaponsDataBase _weaponsDataBase;
        
        private readonly Dictionary<WeaponType, Stack<BulletView>> _pool = new();

        public BulletsPoolFactory(BulletsContainerView bulletsContainerView, WeaponsDataBase weaponsDataBase)
        {
            _bulletsContainerView = bulletsContainerView;
            _weaponsDataBase = weaponsDataBase;
        }

        public void ClearPool() 
        {
            foreach (var bulletList in _pool.Values)
            {
                foreach (var bulletView in bulletList)
                {
                    Object.Destroy(bulletView);
                }
            }
            
            _pool.Clear();
        }

        public BulletView TakeNextBullet(WeaponType weaponType) 
        {
            BulletView nextBullet;

            if (_pool.TryGetValue(weaponType, out var bulletList) == false)
            {
                bulletList = new Stack<BulletView>();

                _pool[weaponType] = bulletList;
            }

            if (bulletList.Count == 0)
            {
                var bulletViewPrefab = CreateNewBullet(weaponType);
                
                nextBullet = Object.Instantiate(bulletViewPrefab, _bulletsContainerView.transform);
            }
            else
            {
                nextBullet = bulletList.Pop();
            }

            return nextBullet;
        }

        public void RecycleBullet(BulletView bulletView)
        {
            bulletView.Recycle();

            _pool[bulletView.WeaponType].Push(bulletView);
        }

        private BulletView CreateNewBullet(WeaponType weaponType)
        {
            return _weaponsDataBase.GetWeaponRecordByType(weaponType)._bulletViewPrefab;
        }
    }
}