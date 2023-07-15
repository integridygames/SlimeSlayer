using Game.Gameplay.Views.Weapon;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class GameScreenView : ViewBase
    {
        [SerializeField] private UiButton _toPauseScreenButton;
        [SerializeField] private ProgressBarView _leftReloadBar;
        [SerializeField] private ProgressBarView _rightReloadBar;
        [SerializeField] private ProgressBarView _levelProgressBar;

        public UiButton ToPauseScreenButton => _toPauseScreenButton;

        public ProgressBarView LeftReloadBar => _leftReloadBar;

        public ProgressBarView RightReloadBar => _rightReloadBar;

        public ProgressBarView LevelProgressBar => _levelProgressBar;
    }
}
