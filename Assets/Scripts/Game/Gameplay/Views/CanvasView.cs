using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views
{
    public class CanvasView : ViewBase
    {
        [SerializeField] private Transform _canvasPoolRoot;

        private Canvas _canvas;

        public Canvas Canvas => _canvas ??= GetComponent<Canvas>();

        public Transform CanvasPoolRoot => _canvasPoolRoot;
    }
}