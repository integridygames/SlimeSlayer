using System;
using Game.Gameplay.UnityComponents;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens
{
    public class SettingsPopupView : ViewBase
    {
        public event Action OnCloseButtonPressed;
        public event Action<bool> OnSoundValueChanged;
        public event Action<bool> OnVibroValueChanger;

        [SerializeField] private UiButton _closeButton;
        [SerializeField] private UiSwitcher _soundSwitcher;
        [SerializeField] private UiSwitcher _vibroSwitcher;

        protected override void OnEnable()
        {
            base.OnEnable();

            _closeButton.OnReleased += OnCloseButtonPressed;
            _soundSwitcher.OnValueChanged += OnSoundValueChanged;
            _vibroSwitcher.OnValueChanged += OnVibroValueChanger;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _closeButton.OnReleased -= OnCloseButtonPressed;
            _soundSwitcher.OnValueChanged -= OnSoundValueChanged;
            _vibroSwitcher.OnValueChanged -= OnVibroValueChanger;
        }

        public void SetSoundState(bool value)
        {
            _soundSwitcher.SetState(value);
        }

        public void SetVibroState(bool value)
        {
            _vibroSwitcher.SetState(value);
        }
    }
}