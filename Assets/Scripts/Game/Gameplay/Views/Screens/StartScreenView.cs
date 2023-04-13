using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.Screens
{
    public class StartScreenView : ViewBase
    {
        [SerializeField] private UiButton _startGameButton;
        [SerializeField] private UiButton _shopButton;
        [SerializeField] private UiButton _statsButton;
        [SerializeField] private UiButton _invButton;
        [SerializeField] private UiButton _achievesButton;

        public UiButton StartGameButton => _startGameButton;

        public UiButton ShopButton => _shopButton;

        public UiButton StatsButton => _statsButton;

        public UiButton InvButton => _invButton;

        public UiButton AchievesButton => _achievesButton;
    }
}