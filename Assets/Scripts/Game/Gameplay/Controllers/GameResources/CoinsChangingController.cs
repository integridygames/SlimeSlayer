using System;
using Game.DataBase.GameResource;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Services;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameResources
{
    public class CoinsChangingController : ControllerBase<GameResourceData>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;
        private readonly LevelInfo _levelInfo;

        public CoinsChangingController(GameResourceData controlledEntity, ApplicationData applicationData, LevelInfo levelInfo) : base(controlledEntity)
        {
            _applicationData = applicationData;
            _levelInfo = levelInfo;
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
                _applicationData.PlayerData.CurrentCoinsCount = ControlledEntity.GetResourceQuantity(GameResourceType.Coin);
                SaveLoadDataService.Save(_applicationData.PlayerData);
                _levelInfo.GoldEarned += value;
            }
        }
    }
}