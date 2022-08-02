using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.Character;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character 
{
    public class NearestHeapFinderSystem : IUpdateSystem
    {
        private CharacterView _characterView;
        private List<List<Collider>> _fieldOfViewParts;
        private HeapInfo _heapInfo;
        private RaycastInfo _raycastInfo;
  
        private const int _circlePartsQuantity = 4;

        public NearestHeapFinderSystem(CharacterView characterView, HeapInfo heapInfo, RaycastInfo raycastInfo) 
        {
            _characterView = characterView;
            _heapInfo = heapInfo;
            _raycastInfo = raycastInfo;
            _fieldOfViewParts = new List<List<Collider>>();
            for(int i = 0; i < _circlePartsQuantity; i++)
                _fieldOfViewParts.Add(new List<Collider>());
        }

        public void Update()
        {
            _heapInfo.HeapVector = DetectNearestEnemiesHeap();
        }

        private Vector3 DetectNearestEnemiesHeap()
        {            
            Collider[] targets = Physics.OverlapSphere(_characterView.transform.position, _raycastInfo.Radius, _raycastInfo.LayerNumber);

            if(targets.Length > 0) 
            {
                TryToFindEnemies(targets);
                Dictionary<int, Vector3> averageVectors = CalculateAverageHeapsVectors();

                Dictionary<int, float> distances = CalculateHeapDistancesToCharacter(averageVectors);
                int minIndex = FindMinDistancesIndex(distances);
                return averageVectors[minIndex];
            }
            else
                return Vector3.zero;
        }

        private void TryToFindEnemies(Collider[] targets) 
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

        private Dictionary<int, Vector3> CalculateAverageHeapsVectors() 
        {
            Vector3 averageVector = Vector3.zero;
            Vector3 vectorsSummary;       

            Dictionary<int, Vector3> averageVectors = new Dictionary<int,Vector3>();

            for (int i = 0; i< _fieldOfViewParts.Count; i++) 
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

                    averageVectors.Add(i, averageVector);
                }              
            }

            return averageVectors;
        }

        private Dictionary<int, float> CalculateHeapDistancesToCharacter(Dictionary<int, Vector3> averageVectors) 
        {
            float currentDistance = 0;
            Dictionary<int, float> distances = new Dictionary<int, float>();

            foreach (var averageVector in averageVectors) 
            {
                currentDistance = Vector3.Distance(_characterView.transform.position, averageVector.Value);
                distances.Add(averageVector.Key, currentDistance);
            }

            return distances;
        }

        private int FindMinDistancesIndex(Dictionary<int, float> distances) 
        {
            float minDistance = float.MaxValue;
            int minIndex = 0;           

            foreach(var distance in distances) 
            {
                if(distance.Value < minDistance) 
                {
                    minDistance = distance.Value;
                    minIndex = distance.Key;
                }

            }

            return minIndex;
        }
    }
}