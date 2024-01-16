using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using System;
using Game.DataBase.GameResource;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Models.Zone;
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
        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity,
            GameResourcePoolFactory gameResourcePoolFactory,
            ActiveEssencesContainer activeEssencesContainer, ActiveCoinsContainer activeCoinsContainer,
            ActiveEnemiesContainer activeEnemiesContainer,
            SpawnZonesDataContainer spawnZonesDataContainer) : base(controlledEntity)
        {
            _gameResourcePoolFactory = gameResourcePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
            _activeCoinsContainer = activeCoinsContainer;
            _activeEnemiesContainer = activeEnemiesContainer;
            _spawnZonesDataContainer = spawnZonesDataContainer;
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

            RecycleSpawnZones();
        }

        private GameResourceViewBase SpawnGameResource(Vector3 position, GameResourceType gameResourceType)
        {
            var view = _gameResourcePoolFactory.GetElement(gameResourceType);
            view.transform.position = position;

            return view;
        }

        private void RecycleSpawnZones()
        {
            if (_activeEnemiesContainer.ActiveEnemies.Count == 0)
            {
                foreach (var zoneData in _spawnZonesDataContainer.SpawnZonesData)
                {
                    zoneData.Recycle();
                }
            }
        }
    }
}