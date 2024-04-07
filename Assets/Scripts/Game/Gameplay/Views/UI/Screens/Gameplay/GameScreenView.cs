using Game.Gameplay.Views.Weapon;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Gameplay
{
    public class GameScreenView : ViewBase
    {
        [SerializeField] private UiButton _toPauseScreenButton;
        [SerializeField] private ProgressBarView _leftReloadBar;
        [SerializeField] private ProgressBarView _rightReloadBar;
        [SerializeField] private ProgressBarView _levelProgressBar;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _timeToNextWave;

        public UiButton ToPauseScreenButton => _toPauseScreenButton;

        public ProgressBarView LeftReloadBar => _leftReloadBar;

        public ProgressBarView RightReloadBar => _rightReloadBar;

        public ProgressBarView LevelProgressBar => _levelProgressBar;

        public TMP_Text Level => _level;
        
        public TMP_Text TimeToNextWave => _timeToNextWave;
    }
}
