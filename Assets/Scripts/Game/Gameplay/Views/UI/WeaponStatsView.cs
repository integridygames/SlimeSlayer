using System.Globalization;
using Game.DataBase.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI
{
    public class WeaponStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _nextValue;
        [SerializeField] private Color _firstColor;
        [SerializeField] private Color _secondColor;
        [SerializeField] private Image _background;

        private static bool _colorChanger;

        public void SetData(WeaponCharacteristicType characteristicType, float currentValue, float nextValueAddition)
        {
            _name.text = characteristicType.ToString();
            _currentValue.text = currentValue.ToString(CultureInfo.InvariantCulture);
            _nextValue.text = $"+{nextValueAddition}";
            _background.color = _colorChanger ? _firstColor : _secondColor;
            _colorChanger = !_colorChanger;
        }
    }
}