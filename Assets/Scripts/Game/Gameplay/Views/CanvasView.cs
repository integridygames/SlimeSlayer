using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views
{
    public class CanvasView : ViewBase
    {
        private Canvas _canvas;

        public Canvas Canvas => _canvas ??= GetComponent<Canvas>();
    }
}