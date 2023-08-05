using Game.Gameplay.Models.Camera;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Camera
{
    public class CameraTargetFinderSystem : IUpdateSystem
    {
        private readonly HeapInfo _heapInfo;
        private readonly CharacterView _characterView;
        private readonly CameraStats _cameraStats;
        private readonly CameraRepository _cameraRepository;
        private readonly CameraContainerView _cameraContainerView;

        public CameraTargetFinderSystem(HeapInfo heapInfo, CharacterView characterView, CameraStats cameraStats,
            CameraRepository cameraRepository, CameraContainerView cameraContainerView)
        {
            _heapInfo = heapInfo;
            _characterView = characterView;
            _cameraStats = cameraStats;
            _cameraRepository = cameraRepository;
            _cameraContainerView = cameraContainerView;
        }

        public void Update()
        {
            if (_heapInfo.FoundHeap)
            {
                var characterPosition = _characterView.transform.position;
                _cameraRepository.CameraTargetPosition =
                    characterPosition + (_heapInfo.ClosestHeapPosition - characterPosition) / 4;
                _cameraRepository.Offset = _cameraContainerView.OffsetDirection * _cameraStats.InBattleCameraDistance;
                _cameraRepository.Speed = _cameraStats.CameraToTargetSpeed;
            }
            else
            {
                _cameraRepository.CameraTargetPosition = _characterView.transform.position;
                _cameraRepository.Offset = Vector3.zero;
                _cameraRepository.Speed = _cameraStats.CameraSpeed;
            }
        }
    }
}