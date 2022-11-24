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
        private readonly ZonesDataContainer _zonesDataContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity, EssencePoolFactory essencePoolFactory,
            ActiveEssencesContainer activeEssencesContainer, ActiveEnemiesContainer activeEnemiesContainer,
            ZonesDataContainer zonesDataContainer) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
            _activeEnemiesContainer = activeEnemiesContainer;
            _zonesDataContainer = zonesDataContainer;
        }

        public void Initialize()
        {
            ControlledEntity.OnEnemyDied += OnEnemyDiedHandler;
            ControlledEntity.OnLastInZoneEnemyDied += OnLastInZoneEnemyDiedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnEnemyDied -= OnEnemyDiedHandler;
            ControlledEntity.OnLastInZoneEnemyDied -= OnLastInZoneEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(EssenceType essenceType, EnemyBase enemy)
        {
            _activeEnemiesContainer.RemoveEnemy(enemy, enemy.ZoneId);

            var essenceView = _essencePoolFactory.GetElement(essenceType);
            essenceView.transform.position = enemy.Position;
            _activeEssencesContainer.AddEssence(essenceView);

            enemy.Remove();
        }

        private void OnLastInZoneEnemyDiedHandler(int zoneId)
        {
            if (_zonesDataContainer.ZonesData[zoneId] is BattlefieldZoneData battlefieldZoneData)
            {
                battlefieldZoneData.Recycle();
                return;
            }

            Debug.LogError(
                $"{nameof(EnemiesController)}.{nameof(OnLastInZoneEnemyDiedHandler)} wrong zone type for enemy death");
        }
    }
}