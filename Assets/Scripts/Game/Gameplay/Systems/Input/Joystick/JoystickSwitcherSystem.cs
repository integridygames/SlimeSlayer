using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Models.Raycast;

namespace Game.Gameplay.Systems.Input.Joystick
{
    public class JoystickSwitcherSystem : JoystickUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;

        public JoystickSwitcherSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo) : base(gameScreenView, mouseRaycastInfo)
        {
            _joystick = joystick;
        }

        public override void Update()
        {
            if (GameScreenView.gameObject.activeInHierarchy) 
            {                
                SwitchState(UnityEngine.Input.GetMouseButtonDown(0) && !MouseRaycastInfo.IsMouseOverUI, true);
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