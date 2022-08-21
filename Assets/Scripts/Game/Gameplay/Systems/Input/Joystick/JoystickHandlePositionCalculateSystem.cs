using TegridyCore.Base;

namespace Game.Gameplay.Systems.Input.Joystick 
{
    public class JoystickHandlePositionCalculateSystem : IUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;

        public JoystickHandlePositionCalculateSystem(Views.Input.Joystick joystick) 
        {
            _joystick = joystick;
        }

        public void Update()
        {
            if (_joystick.gameObject.activeInHierarchy) 
            {                
                _joystick.CalculateHandleLocalPosition();
            }
        }
    }
}