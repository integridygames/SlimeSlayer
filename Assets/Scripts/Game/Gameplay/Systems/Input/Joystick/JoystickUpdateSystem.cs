using Game.Gameplay.Models.Raycast;
using Game.Gameplay.Views.SampleScene.Screens;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Input.Joystick
{
    public abstract class JoystickUpdateSystem : IUpdateSystem
    {
        protected readonly GameScreenView GameScreenView;
        protected readonly MouseRaycastInfo MouseRaycastInfo;

        public JoystickUpdateSystem(GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo)
        {
            GameScreenView = gameScreenView;
            MouseRaycastInfo = mouseRaycastInfo;
        }

        public virtual void Update()
        {
            if (GameScreenView.gameObject.activeInHierarchy && !MouseRaycastInfo.IsMouseOverUI)
            {
                DoUpateMethod();
            }
        }

        protected virtual void DoUpateMethod()
        {

        }
    }
}