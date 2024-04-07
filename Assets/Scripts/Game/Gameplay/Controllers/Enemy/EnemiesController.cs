using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using System;
using Game.DataBase.GameResource;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Views.GameResources;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Enemy
{
    public class EnemiesController : ControllerBase<ActiveEnemiesContainer>, IInitializable, IDisposable
    {
        private readonly GameResourcePoolFactory _gameResourcePoolFactory;
        private readonly ActiveEssencesContainer _activeEssencesContainer;
        private readonly ActiveCoinsContainer _activeCoinsContainer;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity,
            GameResourcePoolFactory gameResourcePoolFactory,
            ActiveEssencesContainer activeEssencesContainer, ActiveCoinsContainer activeCoinsContainer,
            ActiveEnemiesContainer activeEnemiesContainer) : base(controlledEntity)
        {
            _gameResourcePoolFactory = gameResourcePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
            _activeCoinsContainer = activeCoinsContainer;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Initialize()
        {
            ControlledEntity.OnEnemyDied += OnEnemyDiedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(EnemyBase enemy)
        {
            _activeEssencesContainer.Add((EssenceView) SpawnGameResource(enemy.Position, GameResourceType.Essence));
            _activeCoinsContainer.Add((CoinView) SpawnGameResource(enemy.Position, GameResourceType.Coin));
        }

        private GameResourceViewBase SpawnGameResource(Vector3 position, GameResourceType gameResourceType)
        {
            var view = _gameResourcePoolFactory.GetElement(gameResourceType);
            view.transform.position = new Vector3(position.x, 1, position.z);

            return view;
        }
    }
}