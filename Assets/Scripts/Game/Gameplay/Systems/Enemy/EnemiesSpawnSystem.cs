using System;
using System.Collections.Generic;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesSpawnSystem : IUpdateSystem, IInitializeSystem
    {
        private const int CountInBranch = 3;

        private readonly ZonesDataContainer _zonesDataContainer;
        private readonly EnemyFactory _enemyFactory;
        private readonly CharacterView _characterView;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        private List<Tuple<EnemyType, EssenceType>> _enemiesToSpawn;

        public EnemiesSpawnSystem(ZonesDataContainer zonesDataContainer, EnemyFactory enemyFactory,
            CharacterView characterView, ActiveEnemiesContainer activeEnemiesContainer)
        {
            _zonesDataContainer = zonesDataContainer;
            _enemyFactory = enemyFactory;
            _characterView = characterView;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Initialize()
        {
            foreach (var zoneData in _zonesDataContainer.ZonesData.Values)
            {
                if (zoneData is not BattlefieldZoneData battlefieldZoneData) continue;

                battlefieldZoneData.AbleToSpawn = true;
            }
        }

        public void Update()
        {
            foreach (var zoneData in _zonesDataContainer.ZonesData.Values)
            {
                if (zoneData is not BattlefieldZoneData battlefieldZoneData) continue;

                if (ReadyToSpawn(battlefieldZoneData) || battlefieldZoneData.SpawnInProgress)
                {
                    if (battlefieldZoneData.SpawnInProgress == false)
                    {
                        BeginSpawn(battlefieldZoneData);
                    }

                    if (battlefieldZoneData.SpawnProgressNormalized < 1)
                    {
                        TryToSpawn(battlefieldZoneData);
                    }

                    if (IsSpawnEnded(battlefieldZoneData))
                    {
                        EndSpawn(battlefieldZoneData);
                    }
                }
            }
        }

        public bool ReadyToSpawn(BattlefieldZoneData battlefieldZoneData)
        {
            return battlefieldZoneData.AbleToSpawn && battlefieldZoneData.InBoundsOfSpawn(_characterView.transform.position) &&
                   battlefieldZoneData.CurrentTimeout <= 0;
        }

        public void BeginSpawn(BattlefieldZoneData battlefieldZoneData)
        {
            battlefieldZoneData.SpawnInProgress = true;
            battlefieldZoneData.SpawnProgressNormalized = 0;
            battlefieldZoneData.CurrentProgressPoint = 0;
            battlefieldZoneData.AbleToSpawn = false;
            battlefieldZoneData.CurrentSpawnIndex = 0;

            PrepareRandomizedEnemiesToSpawnList(battlefieldZoneData);
        }

        private void PrepareRandomizedEnemiesToSpawnList(BattlefieldZoneData battlefieldZoneData)
        {
            _enemiesToSpawn = new List<Tuple<EnemyType, EssenceType>>();

            foreach (var battlefieldSpawnSettingsRecord in battlefieldZoneData.BattlefieldSpawnSettings)
            {
                for (var i = 0; i < battlefieldSpawnSettingsRecord._count; i++)
                {
                    _enemiesToSpawn.Add(new Tuple<EnemyType, EssenceType>(battlefieldSpawnSettingsRecord._enemyType,
                        battlefieldSpawnSettingsRecord._essenceType));
                }
            }

            _enemiesToSpawn = _enemiesToSpawn.GetRandomRange();
        }

        public void TryToSpawn(BattlefieldZoneData battlefieldZoneData)
        {
            var spawnProgressValueForOneEnemy = 1f / _enemiesToSpawn.Count;

            if (battlefieldZoneData.CurrentProgressPoint < battlefieldZoneData.SpawnProgressNormalized)
            {
                var branchSpawnIndex = 0;

                while (battlefieldZoneData.CurrentProgressPoint < 1 && branchSpawnIndex < CountInBranch)
                {
                    var spawnPosition = battlefieldZoneData.GetRandomPoint();
                    spawnPosition.y = _characterView.transform.position.y;

                    Spawn(_enemiesToSpawn[battlefieldZoneData.CurrentSpawnIndex], spawnPosition,
                        battlefieldZoneData.ZoneId);

                    battlefieldZoneData.CurrentProgressPoint += spawnProgressValueForOneEnemy;
                    battlefieldZoneData.CurrentSpawnIndex++;

                    branchSpawnIndex++;
                }
            }

            battlefieldZoneData.SpawnProgressNormalized += Time.deltaTime / battlefieldZoneData.SpawnTime;
        }

        private void Spawn(Tuple<EnemyType, EssenceType> enemyInfo, Vector3 position, int zoneId)
        {
            var enemy = _enemyFactory.Create(enemyInfo.Item1, enemyInfo.Item2, position, zoneId);

            _activeEnemiesContainer.AddEnemy(enemy, zoneId);
        }

        public bool IsSpawnEnded(BattlefieldZoneData battlefieldZoneData)
        {
            return battlefieldZoneData.SpawnInProgress && battlefieldZoneData.SpawnProgressNormalized >= 1;
        }

        public void EndSpawn(BattlefieldZoneData battlefieldZoneData)
        {
            battlefieldZoneData.SpawnInProgress = false;
        }
    }
}