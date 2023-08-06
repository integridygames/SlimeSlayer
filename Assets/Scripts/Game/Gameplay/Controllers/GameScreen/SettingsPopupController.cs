using System;
using Game.Gameplay.Models;
using Game.Gameplay.Views.UI.Screens;
using Game.Services;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class SettingsPopupController : ControllerBase<SettingsPopupView>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;

        public SettingsPopupController(SettingsPopupView controlledEntity, ApplicationData applicationData) : base(controlledEntity)
        {
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            ControlledEntity.OnCloseButtonPressed += OnCloseButtonPressedHandler;
            ControlledEntity.OnSoundValueChanged += OnSoundValueChangedHandler;
            ControlledEntity.OnVibroValueChanger += OnVibroValueChangedHandler;
            _applicationData.PlayerSettings.MusicEnabled.OnUpdate += OnMusicEnabled;
            _applicationData.PlayerSettings.SoundsEnabled.OnUpdate += OnMusicEnabled;
            _applicationData.PlayerSettings.VibrationEnabled.OnUpdate += OnVibroEnabled;

            ControlledEntity.SetSoundState(_applicationData.PlayerSettings.MusicEnabled.Value);
            ControlledEntity.SetVibroState(_applicationData.PlayerSettings.VibrationEnabled.Value);
        }

        public void Dispose()
        {
            ControlledEntity.OnCloseButtonPressed -= OnCloseButtonPressedHandler;
            ControlledEntity.OnSoundValueChanged -= OnSoundValueChangedHandler;
            ControlledEntity.OnVibroValueChanger -= OnVibroValueChangedHandler;
            _applicationData.PlayerSettings.MusicEnabled.OnUpdate -= OnMusicEnabled;
            _applicationData.PlayerSettings.SoundsEnabled.OnUpdate -= OnMusicEnabled;
            _applicationData.PlayerSettings.VibrationEnabled.OnUpdate -= OnVibroEnabled;
        }

        private void OnCloseButtonPressedHandler()
        {
            ControlledEntity.gameObject.SetActive(false);
        }

        private void OnSoundValueChangedHandler(bool value)
        {
            _applicationData.PlayerSettings.MusicEnabled.Value = value;
            _applicationData.PlayerSettings.SoundsEnabled.Value = value;

            SaveLoadDataService.Save(_applicationData.PlayerSettings);
        }

        private void OnVibroValueChangedHandler(bool value)
        {
            _applicationData.PlayerSettings.VibrationEnabled.Value = value;

            SaveLoadDataService.Save(_applicationData.PlayerSettings);
        }

        private void OnMusicEnabled(RxValue<bool> rxValue)
        {
            ControlledEntity.SetSoundState(rxValue.NewValue);
        }

        private void OnVibroEnabled(RxValue<bool> rxValue)
        {
            ControlledEntity.SetVibroState(rxValue.NewValue);
        }
    }
}