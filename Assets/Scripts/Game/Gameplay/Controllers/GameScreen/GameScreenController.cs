using System;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class GameScreenController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        public GameScreenController(GameScreenView gameScreenView) : base(gameScreenView)
        {
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}