using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon 
{  
    public class WeaponInitializeSystem : IInitializeSystem
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly CharacterView _characterView;
        private readonly WeaponFactory _weaponFactory;
        private readonly ApplicationData _applicationData;

        public WeaponInitializeSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData, CharacterView characterView,
            WeaponFactory weaponFactory, ApplicationData applicationData)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _characterView = characterView;
            _weaponFactory = weaponFactory;
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            var weaponSaveDataLeft = _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentLeftWeaponIndex];
            var weaponSaveDataRight = _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentRightWeaponIndex];

            _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value = _weaponFactory.Create(weaponSaveDataLeft, _characterView.LeftWeaponPlacer, true);
            _currentCharacterWeaponsData.CurrentWeaponViewRight.Value = _weaponFactory.Create(weaponSaveDataRight, _characterView.RightWeaponPlacer, false);
        }
    }
}