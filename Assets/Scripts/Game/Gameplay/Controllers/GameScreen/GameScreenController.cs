using System;
using TegridyCore.Base;
using Zenject;
using Game.Gameplay.Views.SampleScene.Screens;

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