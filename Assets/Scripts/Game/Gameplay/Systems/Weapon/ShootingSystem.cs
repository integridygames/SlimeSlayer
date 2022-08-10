using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.Models.Bullets;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Weapon 
{   
    public class ShootingSystem : IUpdateSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly BulletsPool _bulletsPool;
        private readonly Transform _poolTranform;
        private bool _isTimeToShoot;
        private float _currentTimeBeforeShooting;

        private const float MaxDistance = 30f;
        private const int BulletsPerShot = 1;

        public ShootingSystem(WeaponsInfo weaponsInfo, BulletsPool bulletsPool) 
        {
            _weaponsInfo = weaponsInfo;
            _isTimeToShoot = true;
            _currentTimeBeforeShooting = 0;
            _bulletsPool = bulletsPool;
            _bulletsPool.TryGetComponent<Transform>(out _poolTranform);
        }

        public void Update()
        {
            if (_isTimeToShoot) 
            {
                TryToShoot(_weaponsInfo.CurrentWeaponViewLeft.Value);
                TryToShoot(_weaponsInfo.CurrentWeaponViewRight.Value);
                _isTimeToShoot = false;
            }
            else 
            {
                _currentTimeBeforeShooting += Time.deltaTime;
                if (_currentTimeBeforeShooting >= _weaponsInfo.CurrentWeaponViewLeft.Value.DurationValue) 
                {
                    _isTimeToShoot = true;
                    _currentTimeBeforeShooting = 0;
                }
            }
        }

        private void TryToShoot(WeaponView weaponView) 
        {
            if(Physics.Raycast(weaponView.ShootingPointTranform.position, weaponView.ShootingPointTranform.forward, MaxDistance, (int)Layers.Enemy)) 
            {            
                if (!weaponView.IsUnlimited) 
                {
                    if (weaponView.CurrentAmmoQuantity > 0) 
                    {
                        weaponView.RemoveAmmo(BulletsPerShot);
                        Shoot(weaponView);
                    }
                }
                else 
                    Shoot(weaponView);
            }
        }

        private void Shoot(WeaponView weaponView) 
        {
            var bullet = weaponView.Shoot();
            bullet.GetComponent<Transform>().SetParent(_poolTranform);
            _bulletsPool.Bullets.Add(bullet);
        }
    }
}