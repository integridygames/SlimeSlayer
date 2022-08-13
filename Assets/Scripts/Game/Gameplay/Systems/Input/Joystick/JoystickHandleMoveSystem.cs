using UnityEngine;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Models.Raycast;

namespace Game.Gameplay.Systems.Input.Joystick 
{
    public class JoystickHandleMoveSystem : JoystickFixedUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;

        private Vector3 _currentPosition;
        private Vector3 _previousPosition;

        private const float Speed = 2000f;

        public JoystickHandleMoveSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo) : base(gameScreenView, mouseRaycastInfo)
        {
            _joystick = joystick;
            _currentPosition = Vector3.zero;
            _previousPosition = _currentPosition;
        }     

        protected override void DoFixedUpateMethod()
        {                   
            TryToMove();           
        }

        private void TryToMove() 
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Vector3 currentLocalPosition = _joystick.Handle.transform.position -_joystick.transform.position;

                float positionX = UnityEngine.Input.mousePosition.x;
                float positionY = UnityEngine.Input.mousePosition.y;
                float positionZ = _joystick.Handle.localPosition.z;

                _currentPosition = new Vector3(positionX, positionY, positionZ);
                Move();
            }
        }

        private void Move() 
        {
            if(_currentPosition.x != _previousPosition.x || _currentPosition.y != _previousPosition.y) 
            {           
                _joystick.Handle.position = Vector3.MoveTowards(_joystick.Handle.position, _currentPosition, Time.fixedDeltaTime * Speed);
                _previousPosition = _currentPosition;
            }
        }
    }
}