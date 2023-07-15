using System;
using Game.DataBase.Essence;
using Game.DataBase.GameResource;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Services;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameResources
{
    public class CoinsChangingController : ControllerBase<GameResourceData>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;

        public CoinsChangingController(GameResourceData controlledEntity, ApplicationData applicationData) : base(controlledEntity)
        {
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            ControlledEntity.OnResourceQuantityChanged += OnResourceQuantityChanged;
        }

        public void Dispose()
        {
            ControlledEntity.OnResourceQuantityChanged -= OnResourceQuantityChanged;
        }

        private void OnResourceQuantityChanged(GameResourceType resourceType, int value)
        {
            if (resourceType == GameResourceType.Coin)
            {
                _applicationData.PlayerData.CurrentCoinsCount = value;
                SaveLoadDataService.Save(_applicationData.PlayerData);
            }
        }
    }
}