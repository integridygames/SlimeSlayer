using System;
using System.Collections.Generic;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI
{
    public class WeaponInfoView : MonoBehaviour
    {
        public event Action<WeaponData> OnEquip;
        public event Action<WeaponData> OnUpgrade;
        public event Action OnClose;

        [SerializeField] private UiButton _equipButton;
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private UiButton _upgradeButton;

        [SerializeField] private TMP_Text _equipText;
        [SerializeField] private TMP_Text _upgradeText;

        [SerializeField] private WeaponStatsView _weaponStatsViewPrefab;
        [SerializeField] private Transform _weaponStatsRoot;

        private List<WeaponStatsView> _weaponStatsViews = new();

        private WeaponData _weaponData;

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

            foreach (var weaponStatsView in _weaponStatsViews)
            {
                Destroy(weaponStatsView.gameObject);
            }

            _weaponStatsViews.Clear();
        }

        private void OnEquipButtonPressed()
        {
            OnEquip?.Invoke(_weaponData);
        }

        private void OnUpgradeButtonPressed()
        {
            OnUpgrade?.Invoke(_weaponData);
        }

        private void OnCloseButtonPressed()
        {
            OnClose?.Invoke();
        }

        public void SetWeapon(WeaponData weaponData, bool isEquipped, WeaponsCharacteristics weaponsCharacteristics,
            WeaponsDataBase weaponsDataBase)
        {
            _weaponData = weaponData;
            _equipText.text = isEquipped ? "Equipped" : "Equip";

            var upgradePrice = weaponsCharacteristics.GetCharacteristic(weaponData._weaponType, weaponData._rarityType, WeaponCharacteristicType.UpgradePrice);

            _upgradeText.text = $"Upgrade ({upgradePrice})";

            _equipButton.interactable = isEquipped == false;

            var weaponRecord = weaponsDataBase.GetRecordByType(weaponData._weaponType);

            foreach (var weaponCharacteristic in weaponRecord.GetWeaponCharacteristics(weaponData._rarityType))
            {
                if (weaponCharacteristic._hidden)
                {
                    continue;
                }

                var currentValue = weaponsCharacteristics.CalculateCharacteristicValue(weaponCharacteristic,
                    weaponData._rarityType, weaponData._level);

                var nextAddition = weaponsCharacteristics.GetCharacteristicAddition(weaponCharacteristic, weaponData._level + 1);

                var weaponStatsView = Instantiate(_weaponStatsViewPrefab, _weaponStatsRoot);
                weaponStatsView.SetData(weaponCharacteristic._weaponCharacteristicType, currentValue, nextAddition);

                _weaponStatsViews.Add(weaponStatsView);
            }
        }
    }
}