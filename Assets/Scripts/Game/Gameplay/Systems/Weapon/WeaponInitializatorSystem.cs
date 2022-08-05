using Game.Gameplay.Models.Ammo;
using Game.Gameplay.Models.Weapon;
using Game.ScriptableObjects;
using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using Game.Gameplay.Views.Character.Placers;
using TegridyCore.Base;

public class WeaponInitializatorSystem : IInitializeSystem
{
    private WeaponsInfo _weaponsInfo;
    private AmmoInfo _ammoInfo;
    private WeaponsDataBase _weaponsDB;
    private AmmoDataBase _ammoDB;
    private WeaponPlacer _leftPlacerView;
    private WeaponPlacer _rightPlacerView;

    public WeaponInitializatorSystem(WeaponsInfo weaopnsInfo, AmmoInfo ammoInfo, 
        WeaponsDataBase weaponsDB, AmmoDataBase ammoDB, List<WeaponPlacer> weaponPlacers) 
    {
        _weaponsInfo = weaopnsInfo;
        _ammoInfo = ammoInfo;
        _weaponsDB = weaponsDB;
        _ammoDB = ammoDB;

        foreach(var placer in weaponPlacers) 
        {
            if (placer.IsLeft)
                _leftPlacerView = placer;
            else
                _rightPlacerView = placer;
        }
    }

    public void Initialize()
    {
        _weaponsInfo.PlayerArsenal.Add(_weaponsDB.GetWeaponPrefabByIndex(0));
        _weaponsInfo.CurrentWeaponViewLeft = _leftPlacerView.GetComponentInChildren<WeaponView>();
        _weaponsInfo.CurrentWeaponViewRight = _rightPlacerView.GetComponentInChildren<WeaponView>();
        _ammoInfo.AmmoArsenal.Add(_ammoDB.GetAmmoPrefabByIndex(0));
        _ammoInfo.CurrentAmmoView = _ammoInfo.AmmoArsenal[0];
    }
}