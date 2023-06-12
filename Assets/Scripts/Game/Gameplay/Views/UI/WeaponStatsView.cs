using System.Globalization;
using Game.DataBase.Weapon;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI
{
    public class WeaponStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _nextValue;

        public void SetData(WeaponCharacteristicType characteristicType, float currentValue, float nextValueAddition)
        {
            _name.text = characteristicType.ToString();
            _currentValue.text = currentValue.ToString(CultureInfo.InvariantCulture);
            _nextValue.text = $"+{nextValueAddition}";
        }
    }
}