using Game.Gameplay.Models.Camera;
using Game.Gameplay.Views.CameraContainer;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Camera
{
    public class CameraMovementSystem : IFixedUpdateSystem
    {
        private readonly CameraContainerView _cameraContainerView;
        private readonly CameraRepository _cameraRepository;

        public CameraMovementSystem(CameraContainerView cameraContainerView,
            CameraRepository cameraRepository)
        {
            _cameraContainerView = cameraContainerView;
            _cameraRepository = cameraRepository;
        }

        public void FixedUpdate()
        {
            _cameraContainerView.transform.position = Vector3.Lerp(_cameraContainerView.transform.position,
                _cameraRepository.CameraTargetPosition + _cameraRepository.Offset, Time.deltaTime * _cameraRepository.Speed);
        }
    }
}