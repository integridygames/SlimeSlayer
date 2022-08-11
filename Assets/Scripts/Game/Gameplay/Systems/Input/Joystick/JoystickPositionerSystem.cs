using UnityEngine;
using TegridyCore.Base;
using Game.Gameplay.Views.SampleScene.Screens;

namespace Game.Gameplay.Systems.Input.Joystick 
{  
    public class JoystickPositionerSystem : IUpdateSystem
    {
        private readonly RectTransform _joystickRectTransform;
        private readonly GameScreenView _gameScreenView;

        public JoystickPositionerSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView) 
        {
            joystick.TryGetComponent<RectTransform>(out _joystickRectTransform);
            _gameScreenView = gameScreenView;
        }

        public void Update()
        {
            if (_gameScreenView.gameObject.activeInHierarchy) 
            {
                TryToSetPosition();
            }          
        }

        private void TryToSetPosition() 
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                SetJoystickPosiition();
            }
        }

        private void SetJoystickPosiition() 
        {
            Vector3 targetPosition = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y,
                _joystickRectTransform.position.z);
            _joystickRectTransform.position = targetPosition;           
        }

    }
}