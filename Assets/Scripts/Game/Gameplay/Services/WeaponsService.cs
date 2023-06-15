using System.Linq;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.UI;
using UnityEngine;

namespace Game.Gameplay.Services
{
    public class WeaponsService
    {
        private readonly WeaponsDataBase _weaponsDataBase;
        private readonly WeaponCardsDataBase _weaponCardsDataBase;
        private readonly ApplicationData _applicationData;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly WeaponFactory _weaponFactory;
        private readonly CharacterView _characterView;

        public WeaponsService(WeaponsDataBase weaponsDataBase, WeaponCardsDataBase weaponCardsDataBase,
            ApplicationData applicationData, CurrentCharacterWeaponsData currentCharacterWeaponsData,
            WeaponFactory weaponFactory, CharacterView characterView)
        {
            _weaponsDataBase = weaponsDataBase;
            _weaponCardsDataBase = weaponCardsDataBase;
            _applicationData = applicationData;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _weaponFactory = weaponFactory;
            _characterView = characterView;
        }

        public WeaponCardView SpawnWeaponCard(Transform root, PlayerWeaponData playerWeaponData, bool expand = false)
        {
            var weaponCardViewPrefab =
                _weaponCardsDataBase.GetRecordByType(playerWeaponData._rarityType)._weaponCardView;
            var weaponSprite = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType)._weaponSprite;

            var weaponCardView = Object.Instantiate(weaponCardViewPrefab, root);
            weaponCardView.SetWeapon(playerWeaponData, weaponSprite);

            if (expand)
            {
                var rectTransform = (RectTransform) weaponCardView.transform;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
            }

            return weaponCardView;
        }

        public void RefreshWeaponsInHands()
        {
            var weaponSaveDataLeft =
                _applicationData.PlayerData.WeaponsSaveData.First(x =>
                    x._guid == _applicationData.PlayerData.CurrentLeftWeaponGuid);
            var weaponSaveDataRight =
                _applicationData.PlayerData.WeaponsSaveData.First(x =>
                    x._guid == _applicationData.PlayerData.CurrentRightWeaponGuid);

            _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value =
                _weaponFactory.Create(weaponSaveDataLeft, _characterView.LeftWeaponPlacer, true);
            _currentCharacterWeaponsData.CurrentWeaponViewRight.Value =
                _weaponFactory.Create(weaponSaveDataRight, _characterView.RightWeaponPlacer, false);
        }
    }
}