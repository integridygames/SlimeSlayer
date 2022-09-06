using TegridyCore.Base;
using TegridyUtils.UI.Joystick.Base;

namespace Game.Gameplay.Systems.Character.Movement
{
    public class CharacterEndMoveSystem : IDestroySystem
    {
        private readonly Joystick _joystick;

        public CharacterEndMoveSystem(Joystick joystick)
        {
            _joystick = joystick;
        }

        public void Destroy()
        {
            _joystick.ClearInput();
        }
    }
}