using System;
using TegridyCore.Base;
using TegridyUtils.UI.Elements;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Screens.Gameplay
{
    public class DeathScreenView : ViewBase
    {
        public event Action OnCloseButtonPressed;

        [SerializeField] private UiButton _closeButton;
        [SerializeField] private TMP_Text _earned;
        [SerializeField] private TMP_Text _enemiesKilled;

        public void SetData(string earned, string enemiesKilled)
        {
            _earned.text = earned;
            _enemiesKilled.text = enemiesKilled;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _closeButton.OnReleased += OnCloseButtonPressed;
        }

        protected override void OnDisable()
        {
            _closeButton.OnReleased -= OnCloseButtonPressed;

            base.OnDisable();
        }
    }
}