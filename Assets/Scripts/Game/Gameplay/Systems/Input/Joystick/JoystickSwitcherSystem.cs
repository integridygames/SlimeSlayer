using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Models.Raycast;
using UnityEngine;

namespace Game.Gameplay.Systems.Input.Joystick
{
    public class JoystickSwitcherSystem : JoystickUpdateSystem
    {
        private readonly Views.Input.Joystick _joystick;

        public JoystickSwitcherSystem(Views.Input.Joystick joystick, GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo) : base(gameScreenView, mouseRaycastInfo)
        {
            _joystick = joystick;
        }     

        protected override void DoUpdateMethod()
        {
            SwitchState(UnityEngine.Input.GetMouseButtonDown(0) && !MouseRaycastInfo.IsMouseOverUI, true);
            SwitchState(UnityEngine.Input.GetMouseButtonUp(0), false);
        }

        protected override bool CheckIfAllowed()
        {
            return GameScreenView.gameObject.activeInHierarchy;
        }

        private void SwitchState(bool condition, bool isActive) 
        {
            if (condition)
            {
                _joystick.Handle.localPosition = Vector3.zero;
                _joystick.Background.localPosition = Vector3.zero;
                _joystick.gameObject.SetActive(isActive);
            }
        }
    }
}