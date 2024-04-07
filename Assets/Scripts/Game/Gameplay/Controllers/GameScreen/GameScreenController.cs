using System;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using Game.Gameplay.Views.UI.Screens.Gameplay.Abilities;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class GameScreenController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly PauseView _pauseView;
        private readonly ChooseAbilityView _chooseAbilityView;

        public GameScreenController(GameScreenView gameScreenView, PauseView pauseView, ChooseAbilityView chooseAbilityView) : base(gameScreenView)
        {
            _pauseView = pauseView;
            _chooseAbilityView = chooseAbilityView;
        }

        public void Initialize()
        {
            ControlledEntity.ToPauseScreenButton.OnReleased += OnPauseButtonPressedHandler;
            ControlledEntity.OnHide += OnGameScreenShowHandler;
        }

        public void Dispose()
        {
            ControlledEntity.ToPauseScreenButton.OnReleased -= OnPauseButtonPressedHandler;
            ControlledEntity.OnHide -= OnGameScreenShowHandler;
        }

        private void OnGameScreenShowHandler()
        {
            _pauseView.gameObject.SetActive(false);
            ControlledEntity.LeftReloadBar.gameObject.SetActive(false);
            ControlledEntity.RightReloadBar.gameObject.SetActive(false);
            _chooseAbilityView.gameObject.SetActive(false);
            ControlledEntity.TimeToNextWave.gameObject.SetActive(false);
        }

        private void OnPauseButtonPressedHandler()
        {
            _pauseView.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}