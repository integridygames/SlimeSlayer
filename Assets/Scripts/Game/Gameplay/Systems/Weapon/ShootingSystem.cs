using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.Models.Bullets;
using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Models.Character.TargetSystem;

namespace Game.Gameplay.Systems.Weapon 
{   
    public class ShootingSystem : IUpdateSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly BulletsPool _bulletsPool;
        private readonly Transform _poolTranform;
        private bool _isTimeToShootForLeft;
        private bool _isTimeToShootForRight;
        private float _currentTimeBeforeShootingLeft;
        private float _currentTimeBeforeShootingRight;
        private TargetsInfo _targetsInfo;

        private const float MaxDistance = 30f;
        private const int BulletsPerShot = 1;

        public ShootingSystem(WeaponsInfo weaponsInfo, BulletsPool bulletsPool, TargetsInfo targetsInfo) 
        {
            _weaponsInfo = weaponsInfo;
            _currentTimeBeforeShootingLeft = 0;
            _currentTimeBeforeShootingRight = 0;
            _bulletsPool = bulletsPool;
            _bulletsPool.TryGetComponent<Transform>(out _poolTranform);
            _targetsInfo = targetsInfo;
            _isTimeToShootForLeft = true;
            _isTimeToShootForRight = true;
        }

        public void Update()
        {
            if(CheckShootingNecessity()) 
            {
                CheckIfTimeToShoot(_isTimeToShootForLeft, _weaponsInfo.CurrentWeaponViewLeft.Value, Time.deltaTime, out _isTimeToShootForLeft,
                    _currentTimeBeforeShootingLeft, out _currentTimeBeforeShootingLeft);
                CheckIfTimeToShoot(_isTimeToShootForRight, _weaponsInfo.CurrentWeaponViewRight.Value, Time.deltaTime, out _isTimeToShootForRight,
                   _currentTimeBeforeShootingRight, out _currentTimeBeforeShootingRight);
            }
        }

        private void CheckIfTimeToShoot(bool isTimeToShoot, WeaponView weaponView, float deltaTime, out bool isTimeToShootReturned, float currentTimeBeforeShooting,  
            out float currentTimeBeforeShootingReturned) 
        {
            isTimeToShootReturned = isTimeToShoot;
            currentTimeBeforeShootingReturned = currentTimeBeforeShooting;

            switch (isTimeToShoot)
            {
                case true:
                    TryToShoot(weaponView);
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