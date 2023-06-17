using System;
using System.Collections.Generic;
using Game.DataBase.Character;
using Game.Gameplay.Models.Character;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Character
{
    public class StatsScreenView : ViewBase
    {
        public event Action OnCloseButtonPressed;
        public event Action<PlayerCharacteristicData> OnBuyButtonPressed;

        [SerializeField] private UiButton _closeButton;
        [SerializeField] private CharacterCharacteristicView _characterCharacteristicViewPrefab;
        [SerializeField] private Transform _characteristicsRoot;

        private readonly List<CharacterCharacteristicView> _characterCharacteristicViews = new();

        public void CreateCharacteristics(IReadOnlyList<PlayerCharacteristicData> playerCharacteristicsData, CharacterCharacteristics characterCharacteristics,
            int playerCoins)
        {
            foreach (var playerCharacteristicData in playerCharacteristicsData)
            {
                if (playerCharacteristicData._hidden)
                {
                    continue;
                }

                var characterCharacteristicView = Instantiate(_characterCharacteristicViewPrefab, _characteristicsRoot);
                characterCharacteristicView.Init(playerCharacteristicData, characterCharacteristics, playerCoins);

                characterCharacteristicView.OnBuyButtonPressed += OnBuyButtonPressed;

                _characterCharacteristicViews.Add(characterCharacteristicView);
            }
        }

        public void UpdateCharacteristics(CharacterCharacteristics characterCharacteristics, int playerCoins)
        {
            foreach (var characterCharacteristicView in _characterCharacteristicViews)
            {
                characterCharacteristicView.Refresh(characterCharacteristics, playerCoins);
            }
        }

        public void Clear()
        {
            foreach (var characterCharacteristicView in _characterCharacteristicViews)
            {
                characterCharacteristicView.OnBuyButtonPressed -= OnBuyButtonPressed;

                Destroy(characterCharacteristicView.gameObject);
            }

            _characterCharacteristicViews.Clear();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _closeButton.OnReleased += OnCloseButtonPressed;
        }

        protected override void OnDisable()
        {
            _closeButton.OnReleased -= OnCloseButtonPressed;
            base.OnDisable();
        }
    }
}