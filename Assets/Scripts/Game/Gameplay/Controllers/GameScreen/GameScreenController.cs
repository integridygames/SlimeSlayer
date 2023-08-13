using System;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class GameScreenController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly PauseView _pauseView;

        public GameScreenController(GameScreenView gameScreenView, PauseView pauseView) : base(gameScreenView)
        {
            _pauseView = pauseView;
        }

        public void Initialize()
        {
            ControlledEntity.ToPauseScreenButton.OnReleased += OnPauseButtonPressedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.ToPauseScreenButton.OnReleased -= OnPauseButtonPressedHandler;
        }

        private void OnPauseButtonPressedHandler()
        {
            _pauseView.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}