using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class PauseScreenView : ViewBase
    {
        [SerializeField] private UiButton _closeButton;

        public UiButton CloseButton => _closeButton;
    }
}