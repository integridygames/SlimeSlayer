using System;
using Game.DataBase.GameResource;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.UI;
using Game.Services;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameResources
{
    public class CoinsViewController : ControllerBase<CoinsView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly ResourceShortFormsDataBase _resourceShortFormsDataBase;
        private readonly GameResourceData _gameResourceData;

        public CoinsViewController(CoinsView controlledEntity, ApplicationData applicationData, ResourceShortFormsDataBase resourceShortFormsDataBase, GameResourceData gameResourceData) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _resourceShortFormsDataBase = resourceShortFormsDataBase;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            _gameResourceData.OnResourceQuantityChanged += OnResourceQuantityChangedHandler;

            _gameResourceData.SetResource(GameResourceType.Coin, _applicationData.PlayerData.CurrentCoinsCount);
        }

        public void Dispose()
        {
            _gameResourceData.OnResourceQuantityChanged -= OnResourceQuantityChangedHandler;
        }

        private void OnResourceQuantityChangedHandler(GameResourceType gameResourceType, int count)
        {
            if (gameResourceType == GameResourceType.Coin)
            {
                SetCoins(count);
            }
        }

        private void SetCoins(int value)
        {
            var currentForm = _resourceShortFormsDataBase.GetCurrentForm(value);
            ControlledEntity.SetValue(currentForm);
        }
    }
}