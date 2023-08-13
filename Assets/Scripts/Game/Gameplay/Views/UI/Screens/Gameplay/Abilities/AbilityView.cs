using System;
using Game.DataBase.Abilities;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI.Screens.Gameplay.Abilities
{
    public class AbilityView : MonoBehaviour
    {
        public event Action<AbilityRecord> OnAbilitySelected;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _wholeEffect;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _icon;
        [SerializeField] private UiButton _abilityButton;

        private AbilityRecord _abilityRecord;

        public void SetAbilityData(AbilityRecord abilityRecord, int level)
        {
            _abilityRecord = abilityRecord;
            SetName(abilityRecord.Name);
            SetIcon(abilityRecord.AbilitySprite);

            var abilityLevelRecord = abilityRecord.GetInfoForLevel(level);

            SetLevel(level);
            SetWholeEffect(abilityLevelRecord._wholeEffect);
        }

        private void SetName(string abilityName)
        {
            _name.text = abilityName;
        }

        private void SetWholeEffect(string wholeEffect)
        {
            _wholeEffect.text = wholeEffect;
        }

        private void SetLevel(int level)
        {
            _level.text = $"Lv.{level}";
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