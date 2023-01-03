using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.CameraContainer 
{
    public class CameraContainerView : ViewBase
    {
        private Camera _camera;
        private Vector3? _offsetDirection;

        public Camera Camera => _camera ??= GetComponentInChildren<Camera>();

        public Vector3 OffsetDirection => _offsetDirection ??= (Camera.transform.position - transform.position).normalized;
    }  
}