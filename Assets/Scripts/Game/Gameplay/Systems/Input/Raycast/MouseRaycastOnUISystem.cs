using Game.Gameplay.Views.Input.RaycasterUI;
using Game.Gameplay.Models.Raycast;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Input.Raycast 
{   
    public class MouseRaycastOnUISystem : IUpdateSystem
    {
        private readonly RaycasterUI _raycasterUI;
        private readonly MouseRaycastInfo _mouseRaycastInfo;

        public MouseRaycastOnUISystem(RaycasterUI raycasterUI, MouseRaycastInfo mouseRaycastInfo) 
        {
            _raycasterUI = raycasterUI;
            _mouseRaycastInfo = mouseRaycastInfo;
        }

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _mouseRaycastInfo.IsMouseOverUI = _raycasterUI.DetectIfMouseOverUI();
            }
        }
    }
}