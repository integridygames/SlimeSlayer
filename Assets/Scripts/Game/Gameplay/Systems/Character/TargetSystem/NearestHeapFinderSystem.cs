using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.Character;
using System.Collections.Generic;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class NearestHeapFinderSystem : IUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly List<EnemyBase>[] _fieldOfViewParts;
        private readonly HeapInfo _heapInfo;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;


        private const int CirclePartsQuantity = 4;

        public NearestHeapFinderSystem(CharacterView characterView, HeapInfo heapInfo,
            ActiveEnemiesContainer activeEnemiesContainer)
        {
            _characterView = characterView;
            _heapInfo = heapInfo;
            _activeEnemiesContainer = activeEnemiesContainer;
            _fieldOfViewParts = new List<EnemyBase>[CirclePartsQuantity];

            for (var i = 0; i < CirclePartsQuantity; i++)
                _fieldOfViewParts[i] = new List<EnemyBase>();
        }

        public void Update()
        {
            DetectNearestEnemiesHeap();
        }

        private void DetectNearestEnemiesHeap()
        {
            if (AtLeaseOneEnemyInRangeOfCharacter() == false)
            {
                _heapInfo.FoundHeap = false;
                return;
            }

            DetermineFieldOfViewParts();
            var averageVectors = CalculateAverageHeapsVectors();

            var distances = CalculateHeapDistancesToCharacter(averageVectors);
            var minIndex = FindMinDistancesIndex(distances);
            _heapInfo.ClosestHeapPosition = averageVectors[minIndex];
            _heapInfo.FoundHeap = true;
        }

        private bool AtLeaseOneEnemyInRangeOfCharacter()
        {
            foreach (var enemy in _activeEnemiesContainer.ActiveEnemies)
            {
                if (enemy.IsInCharacterRange)
                {
                    return true;
                }
            }

            return false;
        }

        private void DetermineFieldOfViewParts()
        {
            foreach (var fieldOfViewPart in _fieldOfViewParts)
                fieldOfViewPart.Clear();

            foreach (var target in _activeEnemiesContainer.ActiveEnemies)
            {
                if (target.IsInCharacterRange == false)
                {
                    continue;
                }

                float directionX = 0, directionZ = 0;
                Vector3 direction = target.Position - _characterView.transform.position;
                if (direction.x != 0)
                    directionX = direction.x / Mathf.Abs(direction.x);

                if (direction.z != 0)
                    directionZ = direction.z / Mathf.Abs(direction.z);

                switch (directionX, directionZ)
                {
                    case (1, 1):
                        _fieldOfViewParts[0].Add(target);
                        break;
                    case (1, -1):
                        _fieldOfViewParts[1].Add(target);
                        break;
                    case (-1, -1):
                        _fieldOfViewParts[2].Add(target);
                        break;
                    case (-1, 1):
                        _fieldOfViewParts[3].Add(target);
                        break;
                    case (1, 0):
                        _fieldOfViewParts[0].Add(target);
                        _fieldOfViewParts[1].Add(target);
                        break;
                    case (-1, 0):
                        _fieldOfViewParts[2].Add(target);
                        _fieldOfViewParts[3].Add(target);
                        break;
                    case (0, -1):
                        _fieldOfViewParts[1].Add(target);
                        _fieldOfViewParts[2].Add(target);
                        break;
                    case (0, 1):
                        _fieldOfViewParts[0].Add(target);
                        _fieldOfViewParts[3].Add(target);
                        break;
                }
            }
        }

        private List<Vector3> CalculateAverageHeapsVectors()
        {
            Vector3 averageVector = Vector3.zero;
            Vector3 vectorsSummary;

            List<Vector3> averageVectors = new List<Vector3>();

            for (int i = 0; i < _fieldOfViewParts.Length; i++)
            {
                vectorsSummary = Vector3.zero;

                if (_fieldOfViewParts[i].Count > 0)
                {
                    foreach (var enemyBase in _fieldOfViewParts[i])
                        vectorsSummary += enemyBase.Position;

                    if (_fieldOfViewParts[i].Count > 1)
                        averageVector = vectorsSummary / _fieldOfViewParts[i].Count;
                    else
                        averageVector = vectorsSummary;

                    averageVectors.Add(averageVector);
                }
            }

            return averageVectors;
        }

        private List<float> CalculateHeapDistancesToCharacter(List<Vector3> averageVectors)
        {
            var distances = new List<float>();

            foreach (var averageVector in averageVectors)
            {
                var currentDistance = Vector3.Distance(_characterView.transform.position, averageVector);
                distances.Add(currentDistance);
            }

            return distances;
        }

        private int FindMinDistancesIndex(List<float> distances)
        {
            float minDistance = float.MaxValue;
            int minIndex = 0;

            for (int i = 0; i < distances.Count; i++)
            {
                if (distances[i] < minDistance)
                {
                    minDistance = distances[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}