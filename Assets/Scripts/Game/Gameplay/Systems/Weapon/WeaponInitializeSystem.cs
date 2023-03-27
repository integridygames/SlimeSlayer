using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon 
{  
    public class WeaponInitializeSystem : IInitializeSystem
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly CharacterView _characterView;
        private readonly WeaponFactory _weaponFactory;

        public WeaponInitializeSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData, CharacterView characterView,
            WeaponFactory weaponFactory)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _characterView = characterView;
            _weaponFactory = weaponFactory;
        }

        public void Initialize()
        {
            _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value = _weaponFactory.Create(WeaponType.Shotgun, _characterView.LeftWeaponPlacer, true);
            _currentCharacterWeaponsData.CurrentWeaponViewRight.Value = _weaponFactory.Create(WeaponType.Shotgun, _characterView.RightWeaponPlacer, false);
        }
    }
}