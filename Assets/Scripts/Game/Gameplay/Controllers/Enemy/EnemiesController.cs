using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Essence;
using System;
using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Zone;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Enemy
{
    public class EnemiesController : ControllerBase<ActiveEnemiesContainer>, IInitializable, IDisposable
    {
        private readonly EssencePoolFactory _essencePoolFactory;
        private readonly ActiveEssencesContainer _activeEssencesContainer;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity, EssencePoolFactory essencePoolFactory,
            ActiveEssencesContainer activeEssencesContainer, ActiveEnemiesContainer activeEnemiesContainer,
            SpawnZonesDataContainer spawnZonesDataContainer) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
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
            SpawnEssence(enemy.Position);

            _activeEnemiesContainer.RemoveEnemy(enemy);
            enemy.Destroy();

            RecycleSpawnZones();
        }

        private void SpawnEssence(Vector3 position)
        {
            var essenceView = _essencePoolFactory.GetElement(EssenceType.Blue);
            essenceView.transform.position = position;
            _activeEssencesContainer.AddEssence(essenceView);
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