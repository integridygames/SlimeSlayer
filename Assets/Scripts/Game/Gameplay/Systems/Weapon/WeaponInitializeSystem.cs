using Game.Gameplay.Models.Weapon;
using System.Collections.Generic;
using Game.Gameplay.Factories;
using Game.Gameplay.Views.Character.Placers;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon 
{  
    public class WeaponInitializeSystem : IInitializeSystem
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly WeaponFactory _weaponFactory;
        private readonly WeaponPlacer _leftPlacerView;
        private readonly WeaponPlacer _rightPlacerView;

        public WeaponInitializeSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData, List<WeaponPlacer> weaponPlacers,
            WeaponFactory weaponFactory)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _weaponFactory = weaponFactory;

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
            _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value = _weaponFactory.Create(WeaponType.Pistol, _leftPlacerView);
            _currentCharacterWeaponsData.CurrentWeaponViewRight.Value = _weaponFactory.Create(WeaponType.Pistol, _rightPlacerView);
        }
    }
}