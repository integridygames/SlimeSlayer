using System;
using System.Collections.Generic;
using Game.DataBase.Enemies;
using Game.DataBase.GameResource;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesSpawnSystem : IUpdateSystem
    {
        private const int CountInBranch = 3;

        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;
        private readonly EnemyFactory _enemyFactory;
        private readonly CharacterView _characterView;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        private List<Tuple<EnemyType, GameResourceType>> _enemiesToSpawn;

        public EnemiesSpawnSystem(SpawnZonesDataContainer spawnZonesDataContainer, EnemyFactory enemyFactory,
            CharacterView characterView, ActiveEnemiesContainer activeEnemiesContainer)
        {
            _spawnZonesDataContainer = spawnZonesDataContainer;
            _enemyFactory = enemyFactory;
            _characterView = characterView;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Update()
        {
            foreach (var spawnZoneData in _spawnZonesDataContainer.SpawnZonesData)
            {
                if (ReadyToSpawn(spawnZoneData))
                {
                    if (spawnZoneData.SpawnInProgress == false)
                    {
                        BeginSpawn(spawnZoneData);
                    }

                    if (spawnZoneData.SpawnProgressNormalized < 1)
                    {
                        TryToSpawn(spawnZoneData);
                    }

                    if (IsSpawnEnded(spawnZoneData))
                    {
                        EndSpawn(spawnZoneData);
                    }
                }
            }
        }

        public bool ReadyToSpawn(SpawnZoneData spawnZoneData)
        {
            return spawnZoneData.SpawnInProgress || spawnZoneData.CurrentTimeout <= 0;
        }

        public void BeginSpawn(SpawnZoneData spawnZoneData)
        {
            spawnZoneData.SpawnInProgress = true;
            spawnZoneData.SpawnProgressNormalized = 0;
            spawnZoneData.CurrentSpawnIndex = 0;

            PrepareRandomizedEnemiesToSpawnList(spawnZoneData);
        }

        private void PrepareRandomizedEnemiesToSpawnList(SpawnZoneData spawnZoneData)
        {
            _enemiesToSpawn = new List<Tuple<EnemyType, GameResourceType>>();

            foreach (var battlefieldSpawnSettingsRecord in spawnZoneData.BattlefieldSpawnSettings)
            {
                for (var i = 0; i < battlefieldSpawnSettingsRecord._count; i++)
                {
                    _enemiesToSpawn.Add(new Tuple<EnemyType, GameResourceType>(battlefieldSpawnSettingsRecord._enemyType,
                        battlefieldSpawnSettingsRecord._gameResourceType));
                }
            }

            _enemiesToSpawn = _enemiesToSpawn.GetRandomRange();
        }

        public void TryToSpawn(SpawnZoneData spawnZoneData)
        {
            var nextEnemySpawnProgressPoint = 1.0f / _enemiesToSpawn.Count * spawnZoneData.CurrentSpawnIndex;

            if (nextEnemySpawnProgressPoint < spawnZoneData.SpawnProgressNormalized)
            {
                var branchSpawnIndex = 0;

                while (branchSpawnIndex < CountInBranch && spawnZoneData.CurrentSpawnIndex < spawnZoneData.MaxEnemiesCount)
                {
                    var spawnPosition = spawnZoneData.GetRandomPoint();
                    spawnPosition.y = _characterView.transform.position.y;

                    Spawn(_enemiesToSpawn[spawnZoneData.CurrentSpawnIndex], spawnPosition);

                    spawnZoneData.CurrentSpawnIndex++;

                    branchSpawnIndex++;
                }
            }

            spawnZoneData.SpawnProgressNormalized += Time.deltaTime / spawnZoneData.SpawnTime;
        }

        private void Spawn(Tuple<EnemyType, GameResourceType> enemyInfo, Vector3 position)
        {
            var enemy = _enemyFactory.Create(enemyInfo.Item1, enemyInfo.Item2, position);

            _activeEnemiesContainer.AddEnemy(enemy);
        }

        public bool IsSpawnEnded(SpawnZoneData spawnZoneData)
        {
            return spawnZoneData.SpawnInProgress && spawnZoneData.SpawnProgressNormalized >= 1;
        }

        public void EndSpawn(SpawnZoneData spawnZoneData)
        {
            spawnZoneData.SpawnInProgress = false;
        }
    }
}