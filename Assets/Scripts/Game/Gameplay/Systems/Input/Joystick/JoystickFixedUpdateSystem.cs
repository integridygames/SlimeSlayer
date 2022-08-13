using TegridyCore.Base;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.Models.Raycast;

namespace Game.Gameplay.Systems.Input.Joystick 
{
    public abstract class JoystickFixedUpdateSystem : IFixedUpdateSystem
    {
        protected readonly GameScreenView GameScreenView;
        protected readonly MouseRaycastInfo MouseRaycastInfo;

        public JoystickFixedUpdateSystem(GameScreenView gameScreenView, MouseRaycastInfo mouseRaycastInfo)
        {
            GameScreenView = gameScreenView;
            MouseRaycastInfo = mouseRaycastInfo;
        }

        public virtual void FixedUpdate()
        {
            if (GameScreenView.gameObject.activeInHierarchy && !MouseRaycastInfo.IsMouseOverUI)
            {
                DoFixedUpateMethod();
            }
        }

        protected virtual void DoFixedUpateMethod()
        {

        }
    }   
}