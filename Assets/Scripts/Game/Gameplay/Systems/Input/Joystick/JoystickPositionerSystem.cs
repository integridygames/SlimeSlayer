using UnityEngine;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Models.Raycast;

namespace Game.Gameplay.Systems.Input.Joystick 
{  
    public class JoystickPositionerSystem : JoystickUpdateSystem
    {
        private readonly RectTransform _joystickRectTransform;
        private readonly Views.Input.Joystick _joystick;

        public JoystickPositionerSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo) : base(gameScreenView, mouseRaycastInfo)
        {
            joystick.TryGetComponent<RectTransform>(out _joystickRectTransform);
            _joystick = joystick;
        }

        protected override void DoUpdateMethod()
        {           
            TryToSetPosition();                     
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