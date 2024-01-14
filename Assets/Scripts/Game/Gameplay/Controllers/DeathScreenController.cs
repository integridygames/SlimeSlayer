using System;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers
{
    public class DeathScreenController : ControllerBase<DeathScreenView>, IInitializable, IDisposable
    {
        public DeathScreenController(DeathScreenView controlledEntity) : base(controlledEntity)
        {
        }

        public void Initialize()
        {
            ControlledEntity.OnShow += OnShowed;
        }

        public void Dispose()
        {
            ControlledEntity.OnShow -= OnShowed;
        }

        private void OnShowed()
        {
            ControlledEntity.SetData("test", "test");
        }
    }
}