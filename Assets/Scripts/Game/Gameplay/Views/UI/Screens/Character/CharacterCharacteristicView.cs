using System;
using System.Globalization;
using Game.DataBase.Character;
using Game.Gameplay.Models.Character;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI.Screens.Character
{
    public class CharacterCharacteristicView : MonoBehaviour
    {
        public event Action<PlayerCharacteristicData> OnBuyButtonPressed;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private UiButton _buyButton;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _icon;

        private PlayerCharacteristicData _playerCharacteristicData;

        public void Init(PlayerCharacteristicData playerCharacteristicData, CharacterCharacteristics characterCharacteristics, int playerCoins)
        {
            _playerCharacteristicData = playerCharacteristicData;

            Refresh(characterCharacteristics, playerCoins);
        }

        public void Refresh(CharacterCharacteristics characterCharacteristics, int playerCoins)
        {
            var characteristicValue = characterCharacteristics.GetCharacteristic(_playerCharacteristicData);
            var characteristicPrice = characterCharacteristics.GetPrice(_playerCharacteristicData);

            _icon.sprite = characterCharacteristics.GetIcon(_playerCharacteristicData);

            _buyButton.interactable = playerCoins >= characteristicPrice;

            _priceText.text = $"{characteristicPrice}<sprite name=IconMoney>";
            _name.text = _playerCharacteristicData._characterCharacteristicType.ToString();

            _value.text = characteristicValue.ToString(CultureInfo.InvariantCulture);
        }

        private void OnEnable()
        {
            _buyButton.OnReleased += OnBuyButtonPressedHandler;
        }

        private void OnDisable()
        {
            _buyButton.OnReleased -= OnBuyButtonPressedHandler;
        }

        private void OnBuyButtonPressedHandler()
        {
            OnBuyButtonPressed?.Invoke(_playerCharacteristicData);
        }
    }
}