using Game.Gameplay.Models.Ammo;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using UnityEngine;

public class ShootingSystem : IUpdateSystem
{
    private WeaponsInfo _weaponsInfo;
    private AmmoInfo _ammoInfo;
    private bool _isTimeToShoot;
    private float _currentTimeBeforeShooting;

    private const float MaxDistance = 30f;
    private const int BulletsPerShot = 1;

    public ShootingSystem(WeaponsInfo weaponsInfo, AmmoInfo ammoInfo) 
    {
        _weaponsInfo = weaponsInfo;
        _ammoInfo = ammoInfo;
        _isTimeToShoot = true;
        _currentTimeBeforeShooting = 0;
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
            if (!_ammoInfo.CurrentAmmoView.Value.IsUnlimited) 
            {
                if (_ammoInfo.CurrentAmmoView.Value.Quantity > 0) 
                {
                    _ammoInfo.CurrentAmmoView.Value.RemoveAmmo(BulletsPerShot);
                    weaponView.Shoot();
                }
            }
            else
                weaponView.Shoot();
        }
    }
}
