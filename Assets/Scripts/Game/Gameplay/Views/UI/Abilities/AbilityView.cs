using System;
using Game.DataBase.Abilities;
using Game.Gameplay.AbilitiesMechanics;
using Game.Gameplay.Models.Abilities;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI.Abilities
{
    public class AbilityView : MonoBehaviour
    {
        public event Action<AbilityRecord> OnAbilitySelected;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _wholeEffect;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _icon;
        [SerializeField] private UiButton _abilityButton;

        private AbilityRecord _abilityRecord;

        public void SetAbilityData(AbilityRecord abilityRecord, int level)
        {
            _abilityRecord = abilityRecord;
            SetName(abilityRecord.Name);
            SetDescription(abilityRecord.Description);
            SetIcon(abilityRecord.AbilitySprite);

            var abilityLevelRecord = abilityRecord.GetInfoForLevel(level);

            SetLevel(level);
            SetWholeEffect(abilityLevelRecord._wholeEffect);
        }

        private void SetName(string abilityName)
        {
            _name.text = abilityName;
        }

        private void SetDescription(string description)
        {
            _description.text = description;
        }

        private void SetWholeEffect(string wholeEffect)
        {
            _wholeEffect.text = wholeEffect;
        }

        private void SetLevel(int level)
        {
            _level.text = $"Lvl {level}";
        }

        private void SetIcon(Sprite sprite)
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
            OnAbilitySelected?.Invoke(_abilityRecord);
        }
    }
}