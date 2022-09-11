using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

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
            var characterWorldPosition = _characterView.transform.position;

            var screenPosition = _cameraContainerView.Camera.WorldToScreenPoint(characterWorldPosition);
            screenPosition.y += _characterHealthView.VerticalOffset * _canvasView.Canvas.scaleFactor;

            _characterHealthView.transform.position = screenPosition;
        }
    }
}