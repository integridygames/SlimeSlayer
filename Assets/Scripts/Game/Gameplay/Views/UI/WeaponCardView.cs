using System;
using Game.DataBase.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI
{
    public class WeaponCardView : MonoBehaviour
    {
        public event Action<WeaponData> OnWeaponCardPressed;

        [SerializeField] private Image _weaponImage;
        [SerializeField] private Button _button;

        private WeaponData _weaponData;

        public void SetWeapon(WeaponData weaponData, Sprite sprite)
        {
            _weaponData = weaponData;
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
            OnWeaponCardPressed?.Invoke(_weaponData);
        }
    }
}