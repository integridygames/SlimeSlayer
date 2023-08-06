using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class StartScreenView : ViewBase
    {
        [SerializeField] private UiButton _startGameButton;
        [SerializeField] private UiButton _shopButton;
        [SerializeField] private UiButton _statsButton;
        [SerializeField] private UiButton _weaponButton;
        [SerializeField] private UiButton _craftButton;
        [SerializeField] private UiButton _settingsButton;

        public UiButton StartGameButton => _startGameButton;

        public UiButton ShopButton => _shopButton;

        public UiButton StatsButton => _statsButton;

        public UiButton WeaponButton => _weaponButton;

        public UiButton CraftButton => _craftButton;

        public UiButton SettingsButton => _settingsButton;
    }
}