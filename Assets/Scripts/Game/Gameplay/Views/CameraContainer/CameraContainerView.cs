using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.CameraContainer 
{
    public class CameraContainerView : ViewBase
    {
        private Camera _camera;

        public Camera Camera => _camera ??= GetComponentInChildren<Camera>();
    }  
}