using System;
using Game.DataBase.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI
{
    public class WeaponCardView : MonoBehaviour
    {
        public event Action<PlayerWeaponData, WeaponCardView> OnWeaponCardPressed;

        [SerializeField] private Image _weaponImage;
        [SerializeField] private Image _frame;
        [SerializeField] private Button _button;

        private PlayerWeaponData _playerWeaponData;

        public bool IsFrameEnabled
        {
            set => _frame.gameObject.SetActive(value);
        }

        public PlayerWeaponData WeaponData => _playerWeaponData;

        public void SetWeapon(PlayerWeaponData playerWeaponData, Sprite sprite)
        {
            _playerWeaponData = playerWeaponData;
            _weaponImage.sprite = sprite;
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