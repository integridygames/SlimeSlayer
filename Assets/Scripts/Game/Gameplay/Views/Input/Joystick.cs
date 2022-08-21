using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Input 
{ 
    public class Joystick : ViewBase
    {
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;

        private Vector3 _handleLocalPosition;
        private Vector3 _handleNormalizedLocalPosition;

        public Vector3 HandleNormalizedLocalPosition => _handleNormalizedLocalPosition;
        public RectTransform Background => _background;
        public RectTransform Handle => _handle;

        private const float MaxDifferenceX = 118;
        private const float MaxDifferenceY = 118;

        public void CalculateHandleLocalPosition() 
        {
            _handleLocalPosition = _handle.transform.position - _background.transform.position;
            _handleNormalizedLocalPosition = NormalizePosition(_handleLocalPosition);
        }

        public Vector3 ConvertHandleLocalToWorldPosition() 
        {
            return new Vector3(_handleNormalizedLocalPosition.x, 0, _handleNormalizedLocalPosition.y);
        }

        private Vector3 NormalizePosition(Vector3 position) 
        {
            return new Vector3(position.x / MaxDifferenceX, position.y / MaxDifferenceY, 0);
        }
    }
}