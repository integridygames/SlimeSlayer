using System;
using Game.DataBase.Essence;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Views.GameResources;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameResources
{
    public class CoinsCollectController : ControllerBase<ActiveCoinsContainer>, IInitializable, IDisposable
    {
        private readonly GameResourcePoolFactory _gameResourcePoolFactory;
        private readonly GameResourceData _gameResourceData;

        public CoinsCollectController(ActiveCoinsContainer controlledEntity,
            GameResourcePoolFactory gameResourcePoolFactory, GameResourceData gameResourceData) : base(controlledEntity)
        {
            _gameResourcePoolFactory = gameResourcePoolFactory;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            ControlledEntity.OnCoinCollide += OnCoinCollideHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnCoinCollide -= OnCoinCollideHandler;
        }

        private void OnCoinCollideHandler(CoinView coinView)
        {
            coinView.IsMovingToCharacter = false;
            coinView.MovingProgress = 0;

            ControlledEntity.Remove(coinView);
            _gameResourcePoolFactory.RecycleElement(coinView.GameResourceType, coinView);

            _gameResourceData.AddResource(GameResourceType.Coin, 1);
        }
    }
}