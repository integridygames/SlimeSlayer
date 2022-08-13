using Game.Gameplay.Models.Raycast;
using Game.Gameplay.Views.SampleScene.Screens;
using UnityEngine;

namespace Game.Gameplay.Systems.Input.Joystick 
{
    public class JoystickBackgroundMoveSystem : JoystickFixedUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;
        private readonly float _maxDistance;

        private const float Speed = 2000f;

        public JoystickBackgroundMoveSystem(GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo, Views.Input.Joystick joystick) : base(gameScreenView, mouseRaycastInfo)
        {
            _joystick = joystick;
            _maxDistance = _joystick.Background.sizeDelta.x / 2 - _joystick.Handle.sizeDelta.x / 2;
        }

        protected override void DoFixedUpateMethod()
        {        
            TryToMove();          
        }     

        private void TryToMove() 
        {
            if(Vector3.Distance(_joystick.Background.position, _joystick.Handle.position) >= _maxDistance) 
            {
                _joystick.Background.position = Vector3.MoveTowards(_joystick.Background.position, _joystick.Handle.position, Time.fixedDeltaTime * Speed);
            }
        }
    }
}