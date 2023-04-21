using System;
using Game.DataBase.Weapon;
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

        public void SetWeapon(WeaponData weaponData, bool isEquipped)
        {
            _weaponData = weaponData;
            _equipText.text = isEquipped ? "Equipped" : "Equip";
            _upgradeText.text = $"Upgrade ({weaponData._upgradePrice})";

            _equipButton.interactable = isEquipped == false;
        }
    }
}