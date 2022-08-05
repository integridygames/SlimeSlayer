using Game.Gameplay.Models.Ammo;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapons;
using TegridyCore.Base;
using UnityEngine;

public class ShootingSystem : IFixedUpdateSystem
{
    private WeaponsInfo _weaponsInfo;
    private AmmoInfo _ammoInfo;
    private bool _isTimeToShoot;
    private float _currentTimeBeforeShooting;

    private const float _maxDistance = 30f;
    private const int _layerNumber = 1 << 12;
    private const int _bulletsPerShot = 1;

    public ShootingSystem(WeaponsInfo weaponsInfo, AmmoInfo ammoInfo) 
    {
        _weaponsInfo = weaponsInfo;
        _ammoInfo = ammoInfo;
        _isTimeToShoot = true;
        _currentTimeBeforeShooting = 0;
    }

    public void FixedUpdate()
    {
        if (_isTimeToShoot) 
        {
            TryToShoot(_weaponsInfo.CurrentWeaponViewLeft.Value);
            TryToShoot(_weaponsInfo.CurrentWeaponViewRight.Value);
            _isTimeToShoot = false;
        }
        else 
        {
            _currentTimeBeforeShooting += Time.fixedDeltaTime;
            if (_currentTimeBeforeShooting >= _weaponsInfo.CurrentWeaponViewLeft.Value.DurationValue) 
            {
                _isTimeToShoot = true;
                _currentTimeBeforeShooting = 0;
            }
        }
    }

    private void TryToShoot(WeaponView weaponView) 
    {
        if(Physics.Raycast(weaponView.ShootingPointTranform.position, weaponView.ShootingPointTranform.forward, _maxDistance, _layerNumber)) 
        {            
            if (!_ammoInfo.CurrentAmmoView.Value.IsUnlimited) 
            {
                if (_ammoInfo.CurrentAmmoView.Value.Quantity > 0) 
                {
                    _ammoInfo.CurrentAmmoView.Value.RemoveAmmo(_bulletsPerShot);
                    weaponView.Shoot();
                }
            }
            else
                weaponView.Shoot();
        }
    }
}
