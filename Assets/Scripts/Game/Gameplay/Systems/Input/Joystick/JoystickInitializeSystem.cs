using TegridyCore.Base;

namespace Game.Gameplay.Systems.Input.Joystick 
{
    public class JoystickInitializeSystem : IInitializeSystem
    {
        private readonly Views.Input.Joystick _joystick;

        public JoystickInitializeSystem(Views.Input.Joystick joystick) 
        {
            _joystick = joystick;
        }

        public void Initialize()
        {
            _joystick.gameObject.SetActive(false);
        }   
    }   
}