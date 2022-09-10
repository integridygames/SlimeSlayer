using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using UnityEngine;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;
using Game.ScriptableObjects;

namespace Game.Gameplay.Systems.Weapon 
{   
    public class ShootingSystem : IUpdateSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly TargetsInfo _targetsInfo;
        private readonly ActiveBulletsContainer _activeBulletsContainer;
        private readonly WeaponsDataBase _weaponsDataBase;

        private bool _isTimeToShootForLeft;
        private bool _isTimeToShootForRight;
        private float _currentTimeBeforeShootingLeft;
        private float _currentTimeBeforeShootingRight;

        private const float MaxDistance = 30f;
        private const int BulletsPerShot = 1;

        public ShootingSystem(WeaponsInfo weaponsInfo, BulletsPoolFactory bulletsPool, TargetsInfo targetsInfo, ActiveBulletsContainer activeBulletsContainer,
            WeaponsDataBase weaponsDataBase)  
        {
            _weaponsInfo = weaponsInfo;
            _currentTimeBeforeShootingLeft = 0;
            _currentTimeBeforeShootingRight = 0;
            _bulletsPoolFactory = bulletsPool;

            _targetsInfo = targetsInfo;
            _activeBulletsContainer = activeBulletsContainer;
            _isTimeToShootForLeft = true;
            _isTimeToShootForRight = true;
            _weaponsDataBase = weaponsDataBase;
        }

        public void Update()
        {
            if(CheckShootingNecessity()) 
            {
                CheckIfTimeToShoot(_isTimeToShootForLeft, _weaponsInfo.CurrentWeaponViewLeft.Value, Time.deltaTime, _currentTimeBeforeShootingLeft, 
                    out _isTimeToShootForLeft, out _currentTimeBeforeShootingLeft);
                CheckIfTimeToShoot(_isTimeToShootForRight, _weaponsInfo.CurrentWeaponViewRight.Value, Time.deltaTime, _currentTimeBeforeShootingRight, 
                    out _isTimeToShootForRight, out _currentTimeBeforeShootingRight);
            }
        }

        private void CheckIfTimeToShoot(bool isTimeToShoot, WeaponViewBase weaponView, float deltaTime, float currentTimeBeforeShooting, out bool isTimeToShootReturned,  
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
            if (currentTimeBeforeShootingReturned >= _weaponsInfo.CurrentWeaponViewLeft.Value.Duration)
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

        private void TryToShoot(WeaponViewBase weaponView) 
        {
            if(Physics.Raycast(weaponView.ShootingPoint.position, weaponView.ShootingPoint.forward, MaxDistance, (int)Layers.Enemy)) 
            {            
                if (!weaponView.IsAmmoUnlimited) 
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

        private void Shoot(WeaponViewBase weaponView) 
        {
            var bullet = _bulletsPoolFactory.TakeNextElement(weaponView.ID, _weaponsDataBase);
            _activeBulletsContainer.AddBullet(bullet);
            
            weaponView.Shoot(bullet);
        }
    }
}