﻿using System;
using Game.DataBase.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI
{
    public class WeaponCardView : MonoBehaviour
    {
        public event Action<PlayerWeaponData, WeaponCardView> OnWeaponCardPressed;

        [SerializeField] private Image _weaponImage;
        [SerializeField] private Image _equippedImage;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _level;

        private PlayerWeaponData _playerWeaponData;

        public bool IsEquipped
        {
            set => _equippedImage.gameObject.SetActive(value);
        }

        public PlayerWeaponData WeaponData => _playerWeaponData;

        public void SetWeapon(PlayerWeaponData playerWeaponData, Sprite sprite)
        {
            _playerWeaponData = playerWeaponData;
            _weaponImage.sprite = sprite;
            _level.text = $"Lv. {_playerWeaponData._level + 1}";
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonPressedHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonPressedHandler);
        }

        private void OnButtonPressedHandler()
        {
            OnWeaponCardPressed?.Invoke(WeaponData, this);
        }
    }
}