using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Input 
{ 
    public class Joystick : ViewBase
    {
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;

        public RectTransform Background => _background;
        public RectTransform Handle => _handle;
    }
}