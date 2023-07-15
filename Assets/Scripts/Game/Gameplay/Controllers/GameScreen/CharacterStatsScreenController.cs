using System;
using Game.DataBase.Character;
using Game.DataBase.Essence;
using Game.DataBase.GameResource;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.UI.Screens.Character;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class CharacterStatsScreenController : ControllerBase<StatsScreenView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly CharacterCharacteristics _characterCharacteristics;
        private readonly GameResourceData _gameResourceData;

        public CharacterStatsScreenController(StatsScreenView controlledEntity, ApplicationData applicationData,
            CharacterCharacteristics characterCharacteristics, GameResourceData gameResourceData) : base(
            controlledEntity)
        {
            _applicationData = applicationData;
            _characterCharacteristics = characterCharacteristics;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            ControlledEntity.OnShow += StatsScreenShowHandler;
            ControlledEntity.OnHide += StatsScreenHideHandler;
            ControlledEntity.OnBuyButtonPressed += OnBuyButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnShow -= StatsScreenShowHandler;
            ControlledEntity.OnHide -= StatsScreenHideHandler;
            ControlledEntity.OnBuyButtonPressed -= OnBuyButtonPressedHandler;
        }

        private void StatsScreenShowHandler()
        {
            ControlledEntity.CreateCharacteristics(_applicationData.PlayerData.PlayerCharacteristicsData,
                _characterCharacteristics, _gameResourceData.GetResourceQuantity(GameResourceType.Coin));
        }

        private void StatsScreenHideHandler()
        {
            ControlledEntity.Clear();
        }

        private void OnBuyButtonPressedHandler(PlayerCharacteristicData playerCharacteristicData)
        {
            var price = _characterCharacteristics.GetPrice(playerCharacteristicData);
            _gameResourceData.AddResource(GameResourceType.Coin, -price);

            playerCharacteristicData._level++;

            _characterCharacteristics.UpdateCharacteristic(playerCharacteristicData);

            ControlledEntity.UpdateCharacteristics(_characterCharacteristics, _gameResourceData.GetResourceQuantity(GameResourceType.Coin));
        }
    }
}