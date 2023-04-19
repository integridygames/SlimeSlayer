using System;
using System.Collections.Generic;
using Game.DataBase;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
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

        private WeaponCardView _leftWeaponCardView;
        private WeaponCardView _rightWeaponCardView;

        private bool _isLeftWeaponPressed;

        private List<WeaponCardView> _spawnedWeaponCardViews = new();

        public WeaponScreenController(WeaponScreenView controlledEntity, ApplicationData applicationData,
            CurrentCharacterWeaponsData currentCharacterWeaponsData, WeaponsDataBase weaponsDataBase,
            WeaponCardsDataBase weaponCardsDataBase) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
            _weaponsDataBase = weaponsDataBase;
            _weaponCardsDataBase = weaponCardsDataBase;
        }

        public void Initialize()
        {
            SpawnEquippedWeaponCards();

            ControlledEntity.LeftWeaponButton.OnPressedDown += OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnPressedDown += OnRightWeaponButtonPressedHandler;
            ControlledEntity.OnShow += OnWeaponScreenShow;
            ControlledEntity.OnHide += OnWeaponScreenHide;
            ControlledEntity.CloseButton.OnReleased += OnCloseButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.LeftWeaponButton.OnPressedDown -= OnLeftWeaponButtonPressedHandler;
            ControlledEntity.RightWeaponButton.OnPressedDown -= OnRightWeaponButtonPressedHandler;
            ControlledEntity.OnShow -= OnWeaponScreenShow;
            ControlledEntity.OnHide -= OnWeaponScreenHide;
            ControlledEntity.CloseButton.OnReleased -= OnCloseButtonPressedHandler;
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

            ShowWeaponCards();
        }

        private void SpawnEquippedWeaponCards()
        {
            var weaponSaveDataLeft =
                _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentLeftWeaponIndex];
            var weaponSaveDataRight =
                _applicationData.PlayerData.WeaponsSaveData[_applicationData.PlayerData.CurrentRightWeaponIndex];

            _leftWeaponCardView = SpawnWeaponCard(ControlledEntity.LeftWeaponCardRoot, weaponSaveDataLeft._weaponType,
                weaponSaveDataLeft._rarityType);
            _rightWeaponCardView = SpawnWeaponCard(ControlledEntity.RightWeaponCardRoot,
                weaponSaveDataRight._weaponType, weaponSaveDataRight._rarityType);
        }

        private void ShowWeaponCards()
        {
            foreach (var weaponSaveData in _applicationData.PlayerData.WeaponsSaveData)
            {
                _spawnedWeaponCardViews.Add(SpawnWeaponCard(ControlledEntity.WeaponsCardsRoot,
                    weaponSaveData._weaponType, weaponSaveData._rarityType));
            }
        }

        private WeaponCardView SpawnWeaponCard(Transform root, WeaponType weaponType, RarityType rarityType)
        {
            var weaponCardViewPrefab = _weaponCardsDataBase.GetRecordByType(rarityType)._weaponCardView;
            var weaponSprite = _weaponsDataBase.GetRecordByType(weaponType)._weaponSprite;

            var weaponCardView = Object.Instantiate(weaponCardViewPrefab, root);
            weaponCardView.SetWeaponSprite(weaponSprite);

            return weaponCardView;
        }

        private void OnWeaponScreenHide()
        {
            foreach (var weaponCardView in _spawnedWeaponCardViews)
            {
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
    }
}