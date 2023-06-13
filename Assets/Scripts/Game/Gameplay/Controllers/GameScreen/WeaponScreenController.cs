using System;
using System.Collections.Generic;
using Game.DataBase.Essence;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.UI;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore.Base;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class WeaponScreenController : ControllerBase<WeaponScreenView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;
        private readonly WeaponsDataBase _weaponsDataBase;
        private readonly WeaponCardsDataBase _weaponCardsDataBase;
        private readonly CharacterView _characterView;
        private readonly WeaponFactory _weaponFactory;
        private readonly GameResourceData _gameResourceData;

        private WeaponCardView _leftWeaponCardView;
        private WeaponCardView _rightWeaponCardView;

        private bool _isLeftWeaponPressed;

        private readonly List<WeaponCardView> _spawnedWeaponCardViews = new();

        public WeaponScreenController(WeaponScreenView controlledEntity, ApplicationData applicationData,
            CurrentCharacterWeaponsData currentCharacterWeaponsData, WeaponsDataBase weaponsDataBase,
            WeaponCardsDataBase weaponCardsDataBase, CharacterView characterView, WeaponFactory weaponFactory,
            GameResourceData gameResourceData) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _weaponsDataBase = weaponsDataBase;
            _weaponCardsDataBase = weaponCardsDataBase;
            _characterView = characterView;
            _weaponFactory = weaponFactory;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            SpawnEquippedWeaponCards();

            ControlledEntity.CloseButton.OnReleased += OnCloseButtonPressedHandler;
            ControlledEntity.LeftWeaponButton.OnReleased += OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnReleased += OnRightWeaponButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnEquip += OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose += OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade += OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow += OnWeaponScreenShow;
            ControlledEntity.OnHide += OnWeaponScreenHide;
        }

        public void Dispose()
        {
            ControlledEntity.CloseButton.OnReleased -= OnCloseButtonPressedHandler;
            ControlledEntity.LeftWeaponButton.OnReleased -= OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnReleased -= OnRightWeaponButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnEquip -= OnEquipButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnClose -= OnWeaponInfoCloseButtonPressedHandler;
            ControlledEntity.WeaponInfoView.OnUpgrade -= OnUpgradeButtonPressedHandler;
            ControlledEntity.OnShow -= OnWeaponScreenShow;
            ControlledEntity.OnHide -= OnWeaponScreenHide;
        }

        private void OnLeftWeaponButtonPressedHandler()
        {
            _isLeftWeaponPressed = true;

            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(true);
        }

        private void OnRightWeaponButtonPressedHandler()
        {
            _isLeftWeaponPressed = false;

            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(true);
        }

        private void OnWeaponScreenShow()
        {
            ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(false);
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);

            ShowWeaponCards();
        }

        private void SpawnEquippedWeaponCards()
        {
            var weaponSaveDataLeft =
                _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentLeftWeaponIndex];
            var weaponSaveDataRight =
                _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentRightWeaponIndex];

            _leftWeaponCardView = SpawnWeaponCard(ControlledEntity.LeftWeaponCardRoot,
                weaponSaveDataLeft);
            _rightWeaponCardView = SpawnWeaponCard(ControlledEntity.RightWeaponCardRoot,
                weaponSaveDataRight);
        }

        private void ShowWeaponCards()
        {
            foreach (var weaponSaveData in _applicationData.PlayerData.WeaponsSaveData)
            {
                var weaponCardView = SpawnWeaponCard(ControlledEntity.WeaponsCardsRoot,
                    weaponSaveData);

                _spawnedWeaponCardViews.Add(weaponCardView);

                weaponCardView.OnWeaponCardPressed += OnWeaponCardPressedHandler;
            }
        }

        private WeaponCardView SpawnWeaponCard(Transform root, PlayerWeaponData playerWeaponData)
        {
            var weaponCardViewPrefab =
                _weaponCardsDataBase.GetRecordByType(playerWeaponData._rarityType)._weaponCardView;
            var weaponSprite = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType)._weaponSprite;

            var weaponCardView = Object.Instantiate(weaponCardViewPrefab, root);
            weaponCardView.SetWeapon(playerWeaponData, weaponSprite);

            return weaponCardView;
        }

        private void OnWeaponScreenHide()
        {
            foreach (var weaponCardView in _spawnedWeaponCardViews)
            {
                weaponCardView.OnWeaponCardPressed -= OnWeaponCardPressedHandler;

                Object.Destroy(weaponCardView.gameObject);
            }

            _spawnedWeaponCardViews.Clear();
        }

        private void OnCloseButtonPressedHandler()
        {
            if (ControlledEntity.WeaponsCardsContainer.activeSelf)
            {
                ControlledEntity.WeaponsCardsContainer.gameObject.SetActive(false);
            }
        }

        private void OnWeaponCardPressedHandler(PlayerWeaponData playerWeaponData)
        {
            SetDataToWeaponInfoView(playerWeaponData);

            ControlledEntity.WeaponInfoView.gameObject.SetActive(true);
        }

        private void OnUpgradeButtonPressedHandler(PlayerWeaponData playerWeaponData)
        {
            var price = (int) _currentCharacterWeaponsData.WeaponsCharacteristics.GetCharacteristic(
                playerWeaponData, WeaponCharacteristicType.UpgradePrice);

            _gameResourceData.AddResource(GameResourceType.Coin, -price);

            playerWeaponData._level++;
            var weaponRecord = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);

            foreach (var weaponCharacteristic in weaponRecord.GetWeaponCharacteristics(playerWeaponData._rarityType))
            {
                var currentWeaponCharacteristic =
                    _currentCharacterWeaponsData.WeaponsCharacteristics.CalculateCharacteristicValue(
                        weaponCharacteristic, playerWeaponData._rarityType, playerWeaponData._level);

                _currentCharacterWeaponsData.WeaponsCharacteristics.SetCharacteristic(playerWeaponData,
                    weaponCharacteristic._weaponCharacteristicType, currentWeaponCharacteristic);
            }

            SetDataToWeaponInfoView(playerWeaponData);
        }

        private void SetDataToWeaponInfoView(PlayerWeaponData playerWeaponData)
        {
            var isEquipped = playerWeaponData == _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value.Data ||
                             playerWeaponData == _currentCharacterWeaponsData.CurrentWeaponViewRight.Value.Data;

            ControlledEntity.WeaponInfoView.SetWeapon(playerWeaponData, isEquipped,
                _currentCharacterWeaponsData.WeaponsCharacteristics, _weaponsDataBase,
                _applicationData.PlayerData.CurrentCoinsCount);
        }

        private void OnEquipButtonPressedHandler(PlayerWeaponData playerWeaponData)
        {
            if (_isLeftWeaponPressed)
            {
                _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value.Destroy();
                _currentCharacterWeaponsData.CurrentWeaponViewLeft.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.LeftWeaponPlacer, true);

                Object.Destroy(_leftWeaponCardView.gameObject);
                _leftWeaponCardView = SpawnWeaponCard(ControlledEntity.LeftWeaponCardRoot,
                    playerWeaponData);
            }
            else
            {
                _currentCharacterWeaponsData.CurrentWeaponViewRight.Value.Destroy();
                _currentCharacterWeaponsData.CurrentWeaponViewRight.Value =
                    _weaponFactory.Create(playerWeaponData, _characterView.RightWeaponPlacer, false);

                Object.Destroy(_rightWeaponCardView.gameObject);
                _rightWeaponCardView = SpawnWeaponCard(ControlledEntity.RightWeaponCardRoot,
                    playerWeaponData);
            }

            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
            OnCloseButtonPressedHandler();
        }

        private void OnWeaponInfoCloseButtonPressedHandler()
        {
            ControlledEntity.WeaponInfoView.gameObject.SetActive(false);
        }
    }
}