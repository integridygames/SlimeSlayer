using Game.Gameplay.Models.Camera;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Camera
{
    public class CameraMovementSystem : IFixedUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly CameraContainerView _cameraContainerView;
        private readonly CameraStats _cameraStats;
        private readonly HeapInfo _heapInfo;

        public CameraMovementSystem(CharacterView characterView, CameraContainerView cameraContainerView,
            CameraStats cameraStats, HeapInfo heapInfo)
        {
            _characterView = characterView;
            _cameraContainerView = cameraContainerView;
            _cameraStats = cameraStats;
            _heapInfo = heapInfo;
        }

        public void FixedUpdate()
        {
            Vector3 targetPosition;
            float speed;

            if (_heapInfo.FoundHeap)
            {
                var characterPosition = _characterView.transform.position;
                targetPosition = characterPosition + (_heapInfo.ClosestHeapPosition - characterPosition) / 4;
                speed = _cameraStats.CameraToTargetSpeed;
            }
            else
            {
                targetPosition = _characterView.transform.position;
                speed = _cameraStats.CameraSpeed;
            }

            _cameraContainerView.transform.position = Vector3.Lerp(_cameraContainerView.transform.position,
                targetPosition, Time.deltaTime * speed);
        }
    }
}