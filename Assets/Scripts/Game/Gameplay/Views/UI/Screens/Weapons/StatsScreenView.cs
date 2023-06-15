using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Weapons
{
    public class StatsScreenView : ViewBase
    {
        [SerializeField] private UiButton _closeButton;

        public UiButton CloseButton => _closeButton;
    }
}