using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.Models.Bullets;
using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Models.Character.TargetSystem;
using System.Collections.Generic;

namespace Game.Gameplay.Systems.Weapon 
{   
    public class ShootingSystem : IUpdateSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly BulletsPool _leftBulletsPool;
        private readonly BulletsPool _rightBulletsPool;
        private bool _isTimeToShootForLeft;
        private bool _isTimeToShootForRight;
        private float _currentTimeBeforeShootingLeft;
        private float _currentTimeBeforeShootingRight;
        private TargetsInfo _targetsInfo;

        private const float MaxDistance = 30f;
        private const int BulletsPerShot = 1;

        public ShootingSystem(WeaponsInfo weaponsInfo, List<BulletsPool> bulletsPools, TargetsInfo targetsInfo) 
        {
            _weaponsInfo = weaponsInfo;
            _currentTimeBeforeShootingLeft = 0;
            _currentTimeBeforeShootingRight = 0;

            foreach (var pool in bulletsPools)
            {
                if (pool.IsLeft)
                    _leftBulletsPool = pool;
                else
                    _rightBulletsPool = pool;
            }

            _targetsInfo = targetsInfo;
            _isTimeToShootForLeft = true;
            _isTimeToShootForRight = true;
        }

        public void Update()
        {
            if(CheckShootingNecessity()) 
            {
                CheckIfTimeToShoot(_isTimeToShootForLeft, _weaponsInfo.CurrentWeaponViewLeft.Value, _leftBulletsPool, Time.deltaTime, _currentTimeBeforeShootingLeft, 
                    out _isTimeToShootForLeft, out _currentTimeBeforeShootingLeft);
                CheckIfTimeToShoot(_isTimeToShootForRight, _weaponsInfo.CurrentWeaponViewRight.Value, _rightBulletsPool, Time.deltaTime, _currentTimeBeforeShootingRight, 
                    out _isTimeToShootForRight, out _currentTimeBeforeShootingRight);
            }
        }

        private void CheckIfTimeToShoot(bool isTimeToShoot, WeaponView weaponView, BulletsPool pool, float deltaTime, float currentTimeBeforeShooting, out bool isTimeToShootReturned,  
            out float currentTimeBeforeShootingReturned) 
        {
            isTimeToShootReturned = isTimeToShoot;
            currentTimeBeforeShootingReturned = currentTimeBeforeShooting;

            switch (isTimeToShoot)
            {
                case true:
                    TryToShoot(weaponView, pool);
                    isTimeToShootReturned = false;
                    break;
                case false:
                    CalculateTimeBeforeShooting(deltaTime, currentTimeBeforeShooting, out currentTimeBeforeShootingReturned, out isTimeToShootReturned);
                    break;
            }
        }

        private void CalculateTimeBeforeShooting(float deltaTime, float currentTimeBeforeShooting, out float currentTimeBeforeShootingReturned,
            out bool isTimeToShootReturned) 
        {
            currentTimeBeforeShootingReturned = currentTimeBeforeShooting;
            currentTimeBeforeShootingReturned += deltaTime;
            if (currentTimeBeforeShootingReturned >= _weaponsInfo.CurrentWeaponViewLeft.Value.DurationValue)
            {
                isTimeToShootReturned = true;
                currentTimeBeforeShootingReturned = 0;
            }
            else
                isTimeToShootReturned = false;
        }

        private bool CheckShootingNecessity() 
        {
            return _targetsInfo.Targets.Length > 0 || _isTimeToShootForLeft || _isTimeToShootForRight;
        }

        private void TryToShoot(WeaponView weaponView, BulletsPool pool) 
        {
            if(Physics.Raycast(weaponView.ShootingPointTranform.position, weaponView.ShootingPointTranform.forward, MaxDistance, (int)Layers.Enemy)) 
            {            
                if (!weaponView.IsUnlimited) 
                {
                    if (weaponView.CurrentAmmoQuantity > 0) 
                    {
                        weaponView.RemoveAmmo(BulletsPerShot);
                        Shoot(weaponView, pool);
                    }
                }
                else 
                    Shoot(weaponView, pool);
            }
        }

        private void Shoot(WeaponView weaponView, BulletsPool pool) 
        {
            var bullet = pool.TakeNextBullet();
            weaponView.Shoot(bullet);
        }
    }
}