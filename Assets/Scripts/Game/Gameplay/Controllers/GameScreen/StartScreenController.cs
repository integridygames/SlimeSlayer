using System;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class StartScreenController : ControllerBase<StartScreenView>, IInitializable, IDisposable
    {
        private readonly SettingsPopupView _settingsPopupView;

        public StartScreenController(StartScreenView controlledEntity, SettingsPopupView settingsPopupView) : base(controlledEntity)
        {
            _settingsPopupView = settingsPopupView;
        }


        public void Initialize()
        {
            ControlledEntity.SettingsButton.OnReleased += OnSettingsButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.SettingsButton.OnReleased += OnSettingsButtonPressedHandler;
        }

        private void OnSettingsButtonPressedHandler()
        {
            _settingsPopupView.gameObject.SetActive(true);
        }
    }
}