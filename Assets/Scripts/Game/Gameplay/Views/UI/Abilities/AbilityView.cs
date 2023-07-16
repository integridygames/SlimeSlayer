using System;
using Game.Gameplay.Models.Abilities;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI.Abilities
{
    public class AbilityView : MonoBehaviour
    {
        public event Action<AbilityType> OnAbilitySelected;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _wholeEffect;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _icon;
        [SerializeField] private UiButton _abilityButton;

        private AbilityType _abilityType;

        public void SetAbilityType(AbilityType abilityType)
        {
            _abilityType = abilityType;
        }

        public void SetName(string abilityName)
        {
            _name.text = abilityName;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public void SetWholeEffect(string wholeEffect)
        {
            _wholeEffect.text = wholeEffect;
        }

        public void SetLevel(int level)
        {
            _level.text = $"Lvl {level}";
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        private void OnEnable()
        {
            _abilityButton.OnReleased += OnAbilityButtonPressedHandler;
        }

        private void OnDisable()
        {
            _abilityButton.OnReleased -= OnAbilityButtonPressedHandler;
        }

        private void OnAbilityButtonPressedHandler()
        {
            OnAbilitySelected?.Invoke(_abilityType);
        }
    }
}