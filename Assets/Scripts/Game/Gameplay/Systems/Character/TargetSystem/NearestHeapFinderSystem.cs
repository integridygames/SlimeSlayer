using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Models.Character.TargetSystem;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class NearestHeapFinderSystem : IUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly List<Collider>[] _fieldOfViewParts;
        private readonly HeapInfo _heapInfo;
        private readonly TargetsInfo _targetsInfo;
  
        private const int CirclePartsQuantity = 4;

        public NearestHeapFinderSystem(CharacterView characterView, HeapInfo heapInfo, TargetsInfo targetsInfo) 
        {
            _characterView = characterView;
            _heapInfo = heapInfo;
            _fieldOfViewParts = new List<Collider>[CirclePartsQuantity];
            for(int i = 0; i < CirclePartsQuantity; i++)
                _fieldOfViewParts[i] = new List<Collider>();
            _targetsInfo = targetsInfo;
        }

        public void Update()
        {
            _heapInfo.HeapVector = DetectNearestEnemiesHeap();
        }

        private Vector3 DetectNearestEnemiesHeap()
        {
            Collider[] targets = _targetsInfo.Targets;

            if (targets.Length > 0)
            {
                DetermineFieldOfViewParts(targets);
                List<Vector3> averageVectors = CalculateAverageHeapsVectors();

                List<float> distances = CalculateHeapDistancesToCharacter(averageVectors);
                int minIndex = FindMinDistancesIndex(distances);
                return averageVectors[minIndex];
            }
            else
                return Vector3.zero;
        }

        private void DetermineFieldOfViewParts(Collider[] targets) 
        {
            foreach (var fieldOfViewPart in _fieldOfViewParts)
                fieldOfViewPart.Clear();

            foreach(var target in targets) 
            {
                float directionX = 0, directionZ = 0;
                Vector3 direction = target.transform.position - _characterView.transform.position;
                if(direction.x != 0)
                    directionX = direction.x / Mathf.Abs(direction.x);

                if(direction.z != 0)
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

            for (int i = 0; i< _fieldOfViewParts.Length; i++) 
            {
                vectorsSummary = Vector3.zero;

                if (_fieldOfViewParts[i].Count > 0)
                {
                    foreach (var collider in _fieldOfViewParts[i])
                        vectorsSummary += collider.transform.position;

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
            float currentDistance = 0;
            List<float> distances = new List<float>();

            foreach (var averageVector in averageVectors) 
            {
                currentDistance = Vector3.Distance(_characterView.transform.position, averageVector);
                distances.Add(currentDistance);
            }

            return distances;
        }

        private int FindMinDistancesIndex(List<float> distances) 
        {
            float minDistance = float.MaxValue;
            int minIndex = 0;           

            for(int i = 0; i < distances.Count; i++) 
            {
                if(distances[i] < minDistance) 
                {
                    minDistance = distances[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}