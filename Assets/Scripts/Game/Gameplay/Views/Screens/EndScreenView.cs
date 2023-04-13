using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.Screens
{
    public class EndScreenView : ViewBase
    {
        [SerializeField] private UiButton _completeButton;

        public UiButton CompleteButton => _completeButton;
    }
}