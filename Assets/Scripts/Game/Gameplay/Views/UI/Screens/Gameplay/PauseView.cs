using System;
using System.Collections.Generic;
using Game.DataBase.Abilities;
using Game.Gameplay.Views.UI.Screens.Gameplay.Abilities;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Gameplay
{
    public class PauseView : ViewBase
    {
        public event Action OnCloseButtonPressed;
        public event Action OnExitButtonPressed;
        public event Action OnSettingsButtonPressed;

        [SerializeField] private UiButton _exitButton;
        [SerializeField] private UiButton _settingsButton;
        [SerializeField] private UiButton _closeButton;
        [SerializeField] private SettingsAbilityView _abilityViewPrefab;
        [SerializeField] private Transform _abilitiesRoot;

        private readonly List<SettingsAbilityView> _abilityViews = new();

        public void SetAbilities(List<(AbilityRecord record, int level)> abilitiesData)
        {
            foreach (var abilityView in _abilityViews)
            {
                Destroy(abilityView.gameObject);
            }

            _abilityViews.Clear();

            foreach (var abilityData in abilitiesData)
            {
                var settingsAbilityView = Instantiate(_abilityViewPrefab, _abilitiesRoot);
                settingsAbilityView.SetData(abilityData.record.AbilitySprite, abilityData.level,
                    abilityData.record.Name, abilityData.record.Description);

                _abilityViews.Add(settingsAbilityView);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _closeButton.OnReleased += OnCloseButtonPressed;
            _exitButton.OnReleased += OnExitButtonPressed;
            _settingsButton.OnReleased += OnSettingsButtonPressed;
        }

        protected override void OnDisable()
        {
            _closeButton.OnReleased -= OnCloseButtonPressed;
            _exitButton.OnReleased -= OnExitButtonPressed;
            _settingsButton.OnReleased -= OnSettingsButtonPressed;

            base.OnDisable();
        }
    }
}