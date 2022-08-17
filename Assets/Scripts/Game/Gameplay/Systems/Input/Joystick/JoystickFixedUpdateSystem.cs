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
            if (CheckIfAllowed())
            {
                DoFixedUpateMethod();
            }
        }

        protected virtual bool CheckIfAllowed() 
        {
            return (GameScreenView.gameObject.activeInHierarchy && !MouseRaycastInfo.IsMouseOverUI);
        }

        protected abstract void DoFixedUpateMethod();  
    }   
}