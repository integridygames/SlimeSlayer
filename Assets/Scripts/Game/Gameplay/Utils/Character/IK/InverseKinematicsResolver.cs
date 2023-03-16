using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.Character;
using UnityEngine;

namespace Game.Gameplay.Utils.Character.IK
{
    public class InverseKinematicsResolver
    {
        private readonly HandIKView _handIKView;
        private readonly HeapInfo _heapInfo;

        private float _currentWeight;

        public InverseKinematicsResolver(HandIKView handIKView, HeapInfo heapInfo)
        {
            _handIKView = handIKView;
            _heapInfo = heapInfo;
        }

        public void ResolveIK()
        {
            _currentWeight = Mathf.Lerp(_currentWeight, _heapInfo.FoundHeap ? 1 : 0, Time.deltaTime * 5);

            _handIKView.SetWeight(_currentWeight);
        }
    }
}