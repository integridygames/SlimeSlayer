using Game.Gameplay.Models.Weapon;
using Game.ScriptableObjects;
using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using Game.Gameplay.Views.Character.Placers;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon 
{  
    public class WeaponInitializatorSystem : IInitializeSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly WeaponsDataBase _weaponsDB;
        private readonly WeaponPlacer _leftPlacerView;
        private readonly WeaponPlacer _rightPlacerView;

        public WeaponInitializatorSystem(WeaponsInfo weaopnsInfo, 
            WeaponsDataBase weaponsDB, List<WeaponPlacer> weaponPlacers) 
        {
            _weaponsInfo = weaopnsInfo;
            _weaponsDB = weaponsDB;

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
            _weaponsInfo.CurrentWeaponViewLeft.Value.CurrentAmmoQuantity = _weaponsInfo.CurrentWeaponViewLeft.Value.MaxBulletsQunatity;
            _weaponsInfo.CurrentWeaponViewRight.Value.CurrentAmmoQuantity = _weaponsInfo.CurrentWeaponViewRight.Value.MaxBulletsQunatity;
        }
    }
}