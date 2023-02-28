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
    public class EnemiesSpawnSystem : IUpdateSystem
    {
        private const int CountInBranch = 3;

        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;
        private readonly EnemyFactory _enemyFactory;
        private readonly CharacterView _characterView;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        private List<Tuple<EnemyType, EssenceType>> _enemiesToSpawn;

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
                if (ReadyToSpawn(spawnZoneData) || spawnZoneData.SpawnInProgress)
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
            return spawnZoneData.AbleToSpawn && spawnZoneData.CurrentTimeout <= 0;;
        }

        public void BeginSpawn(SpawnZoneData spawnZoneData)
        {
            spawnZoneData.SpawnInProgress = true;
            spawnZoneData.SpawnProgressNormalized = 0;
            spawnZoneData.CurrentProgressPoint = 0;
            spawnZoneData.AbleToSpawn = false;
            spawnZoneData.CurrentSpawnIndex = 0;

            PrepareRandomizedEnemiesToSpawnList(spawnZoneData);
        }

        private void PrepareRandomizedEnemiesToSpawnList(SpawnZoneData spawnZoneData)
        {
            _enemiesToSpawn = new List<Tuple<EnemyType, EssenceType>>();

            foreach (var battlefieldSpawnSettingsRecord in spawnZoneData.BattlefieldSpawnSettings)
            {
                for (var i = 0; i < battlefieldSpawnSettingsRecord._count; i++)
                {
                    _enemiesToSpawn.Add(new Tuple<EnemyType, EssenceType>(battlefieldSpawnSettingsRecord._enemyType,
                        battlefieldSpawnSettingsRecord._essenceType));
                }
            }

            _enemiesToSpawn = _enemiesToSpawn.GetRandomRange();
        }

        public void TryToSpawn(SpawnZoneData spawnZoneData)
        {
            var spawnProgressValueForOneEnemy = 1f / _enemiesToSpawn.Count;

            if (spawnZoneData.CurrentProgressPoint < spawnZoneData.SpawnProgressNormalized)
            {
                var branchSpawnIndex = 0;

                while (spawnZoneData.CurrentProgressPoint < 1 && branchSpawnIndex < CountInBranch)
                {
                    var spawnPosition = spawnZoneData.GetRandomPoint();
                    spawnPosition.y = _characterView.transform.position.y + 0.5f;

                    Spawn(_enemiesToSpawn[spawnZoneData.CurrentSpawnIndex], spawnPosition);

                    spawnZoneData.CurrentProgressPoint += spawnProgressValueForOneEnemy;
                    spawnZoneData.CurrentSpawnIndex++;

                    branchSpawnIndex++;
                }
            }

            spawnZoneData.SpawnProgressNormalized += Time.deltaTime / spawnZoneData.SpawnTime;
        }

        private void Spawn(Tuple<EnemyType, EssenceType> enemyInfo, Vector3 position)
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