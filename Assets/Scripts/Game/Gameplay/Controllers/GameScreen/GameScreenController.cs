using System;
using TegridyCore.Base;
using Zenject;
using Game.Gameplay.Views.SampleScene.Screens;

namespace Game.Gameplay.Controllers.GameScreen 
{
    public class GameScreenController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly Views.Input.Joystick _joystick;

        public GameScreenController(GameScreenView gameScreenView, Views.Input.Joystick joystick) : base(gameScreenView) 
        {
            _joystick = joystick;
        }

        public void Initialize()
        {
            ControlledEntity.OnHide += DisableJoystick;
        }

        public void Dispose()
        {
            ControlledEntity.OnHide -= DisableJoystick;
        }

        private void DisableJoystick() 
        {
            _joystick.gameObject.SetActive(false);
        }
    } 
}