using Game.Gameplay.Models.Weapon;
using Game.ScriptableObjects;
using Game.Gameplay.Views.Weapons;
using System.Collections.Generic;
using Game.Gameplay.Views.Character.Placers;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon 
{  
    public class WeaponInitializeSystem : IInitializeSystem
    {
        private readonly WeaponsInfo _weaponsInfo;
        private readonly WeaponsDataBase _weaponsDB;
        private readonly WeaponPlacer _leftPlacerView;
        private readonly WeaponPlacer _rightPlacerView;

        public WeaponInitializeSystem(WeaponsInfo weaponsInfo, 
            WeaponsDataBase weaponsDB, List<WeaponPlacer> weaponPlacers) 
        {
            _weaponsInfo = weaponsInfo;
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
            _weaponsInfo.PlayerArsenal.Add(_weaponsDB.GetRecordByIndex(0)._weaponPrefab);
            _weaponsInfo.CurrentWeaponViewLeft = _leftPlacerView.GetComponentInChildren<WeaponViewBase>();
            _weaponsInfo.CurrentWeaponViewRight = _rightPlacerView.GetComponentInChildren<WeaponViewBase>();
            _weaponsInfo.CurrentWeaponViewLeft.Value.AddAmmo(_weaponsInfo.CurrentWeaponViewLeft.Value.MaxBulletsQuantity);
            _weaponsInfo.CurrentWeaponViewRight.Value.AddAmmo(_weaponsInfo.CurrentWeaponViewRight.Value.MaxBulletsQuantity);
        }
    }
}