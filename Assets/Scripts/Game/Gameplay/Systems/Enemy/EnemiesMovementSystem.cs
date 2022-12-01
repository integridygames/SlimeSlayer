using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesMovementSystem : IFixedUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly CharacterView _characterView;

        public EnemiesMovementSystem(ActiveEnemiesContainer activeEnemiesContainer,
            ZonesDataContainer zonesDataContainer, CharacterView characterView)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _zonesDataContainer = zonesDataContainer;
            _characterView = characterView;
        }

        public void FixedUpdate()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                if (activeEnemy.IsOnAttack)
                {
                    continue;
                }

                var zoneData = _zonesDataContainer.ZonesData[activeEnemy.ZoneId];

                if (CharacterInEnemyZone(zoneData))
                {
                    MoveToPlayer(activeEnemy);
                }
                else
                {
                    MoveToRandomPoint(activeEnemy, (BattlefieldZoneData) zoneData);
                }
            }
        }

        private bool CharacterInEnemyZone(ZoneData zoneData)
        {
            return _zonesDataContainer.CurrentZoneData.ZoneId == zoneData.ZoneId &&
                   zoneData.InBoundsOfZone(_characterView.transform.position);
        }

        private void MoveToPlayer(EnemyBase activeEnemy)
        {
            activeEnemy.Target = _characterView.transform.position;
            activeEnemy.UpdateMovement();
        }

        private static void MoveToRandomPoint(EnemyBase activeEnemy, BattlefieldZoneData battlefieldZoneData)
        {
            if (battlefieldZoneData.InBoundsOfSpawn(activeEnemy.Target) &&
                Vector3.Distance(activeEnemy.Target, activeEnemy.Position) >= 5)
            {
                activeEnemy.UpdateMovement();
                return;
            }

            var randomPoint = battlefieldZoneData.GetRandomPoint();
            randomPoint.y = activeEnemy.Position.y;

            activeEnemy.Target = randomPoint;
        }
    }
}