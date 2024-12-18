﻿using System;
using System.Collections.Generic;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Services;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI
{
    public class WeaponInfoView : MonoBehaviour
    {
        public event Action<PlayerWeaponData> OnEquip;
        public event Action<PlayerWeaponData> OnUpgrade;
        public event Action OnClose;

        [SerializeField] private UiButton _equipButton;
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private UiButton _upgradeButton;

        [SerializeField] private TMP_Text _equipText;
        [SerializeField] private TMP_Text _upgradeText;

        [SerializeField] private WeaponStatsView _weaponStatsViewPrefab;
        [SerializeField] private Transform _weaponStatsRoot;
        [SerializeField] private WeaponCardView _weaponCardView;

        private readonly List<WeaponStatsView> _weaponStatsViews = new();

        private PlayerWeaponData _playerWeaponData;

        private void OnEnable()
        {
            _equipButton.OnReleased += OnEquipButtonPressed;
            _closeButton.OnReleased += OnCloseButtonPressed;
            _upgradeButton.OnReleased += OnUpgradeButtonPressed;
        }

        private void OnDisable()
        {
            _equipButton.OnReleased -= OnEquipButtonPressed;
            _closeButton.OnReleased -= OnCloseButtonPressed;
            _upgradeButton.OnReleased -= OnUpgradeButtonPressed;
        }

        private void OnEquipButtonPressed()
        {
            OnEquip?.Invoke(_playerWeaponData);
        }

        private void OnUpgradeButtonPressed()
        {
            OnUpgrade?.Invoke(_playerWeaponData);
        }

        private void OnCloseButtonPressed()
        {
            OnClose?.Invoke();
        }

        public void SetWeapon(PlayerWeaponData playerWeaponData, bool isEquipped,
            WeaponsCharacteristics weaponsCharacteristics, WeaponsDataBase weaponsDataBase, int playerCoins,
            ResourceShortFormsDataBase resourceShortFormsDataBase)
        {
            ClearStats();

            _playerWeaponData = playerWeaponData;
            _equipText.text = isEquipped ? "Equipped" : "Equip";

            var upgradePrice = (int)weaponsCharacteristics.GetCharacteristic(playerWeaponData, WeaponCharacteristicType.UpgradePrice);
            var upgradePriceShortForm = resourceShortFormsDataBase.GetCurrentForm(upgradePrice);

            _upgradeText.text = $"Upgrade (<sprite name=IconMoney>{upgradePriceShortForm})";
            _upgradeButton.interactable = upgradePrice <= playerCoins;

            _equipButton.interactable = isEquipped == false;

            var weaponRecord = weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);

            foreach (var weaponCharacteristic in weaponRecord.GetWeaponCharacteristics(playerWeaponData._rarityType))
            {
                if (weaponCharacteristic._hidden)
                {
                    continue;
                }

                var currentValue = weaponsCharacteristics.CalculateCharacteristicValue(weaponCharacteristic, playerWeaponData._level);

                var nextAddition = weaponsCharacteristics.GetCharacteristicAddition(weaponCharacteristic, playerWeaponData._level + 1);

                var weaponStatsView = Instantiate(_weaponStatsViewPrefab, _weaponStatsRoot);
                weaponStatsView.SetData(weaponCharacteristic._weaponCharacteristicType, currentValue, nextAddition);

                _weaponStatsViews.Add(weaponStatsView);
            }

            _weaponCardView.SetWeapon(_playerWeaponData, weaponRecord._weaponSprite);
            _weaponCardView.IsEquipped = false;
        }

        private void ClearStats()
        {
            foreach (var weaponStatsView in _weaponStatsViews)
            {
                Destroy(weaponStatsView.gameObject);
            }

            _weaponStatsViews.Clear();
        }
    }
}