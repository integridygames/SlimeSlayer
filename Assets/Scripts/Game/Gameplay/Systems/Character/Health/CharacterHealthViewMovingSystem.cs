using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using TegridyUtils;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Health
{
    public class CharacterHealthViewMovingSystem : IUpdateSystem
    {
        private readonly CharacterView _characterView;
        private readonly CharacterHealthView _characterHealthView;
        private readonly CameraContainerView _cameraContainerView;
        private readonly CanvasView _canvasView;

        public CharacterHealthViewMovingSystem(CharacterView characterView, CharacterHealthView characterHealthView,
            CameraContainerView cameraContainerView, CanvasView canvasView)
        {
            _characterView = characterView;
            _characterHealthView = characterHealthView;
            _cameraContainerView = cameraContainerView;
            _canvasView = canvasView;
        }

        public void Update()
        {
            var screenPosition = MathUtils.ToScreenPositionWithOffset(_characterView.transform.position, _cameraContainerView.Camera, _characterHealthView.VerticalOffset, _canvasView.Canvas.scaleFactor);

            _characterHealthView.transform.position = screenPosition;
        }


    }
}