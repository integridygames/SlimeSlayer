using TegridyCore.Base;
using Game.Gameplay.Views.SampleScene.Screens;

namespace Game.Gameplay.Systems.Input.Joystick
{
    public class JoystickSwitcherSystem : IUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;
        private readonly GameScreenView _gameScreenView;

        public JoystickSwitcherSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView) 
        {
            _joystick = joystick;
            _gameScreenView = gameScreenView;
        }

        public void Update()
        {
            if (_gameScreenView.gameObject.activeInHierarchy) 
            {
                SwitchState(UnityEngine.Input.GetMouseButtonDown(0), true);
                SwitchState(UnityEngine.Input.GetMouseButtonUp(0), false);          
            }
        }

        private void SwitchState(bool condition, bool isActive) 
        {
            if (condition)
            {
                _joystick.gameObject.SetActive(isActive);
            }
        }
    }
}