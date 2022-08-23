using Game.Gameplay.Views.CameraContainer;
using TegridyCore.Base;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Systems.CameraContainer 
{
   public class CameraContainerInitializeSystem : IInitializeSystem
    {
        private readonly CharacterView _characterView;
        private readonly CameraContainerView _cameraContainerView;

        public CameraContainerInitializeSystem(CharacterView characterView, CameraContainerView cameraContainerView) 
        {
            _characterView = characterView;
            _cameraContainerView = cameraContainerView;
        }

        public void Initialize()
        {
            _cameraContainerView.transform.position = _characterView.transform.position;
        }
    }  
}