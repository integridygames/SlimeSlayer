using Game.Gameplay.Views.Weapon;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.Screens
{
    public class GameScreenView : ViewBase
    {
        [SerializeField] private UiButton _toPauseScreenButton;
        [SerializeField] private ReloadBarView _leftReloadBar;
        [SerializeField] private ReloadBarView _rightReloadBar;

        public UiButton ToPauseScreenButton => _toPauseScreenButton;

        public ReloadBarView LeftReloadBar => _leftReloadBar;

        public ReloadBarView RightReloadBar => _rightReloadBar;
    }
}
