using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Level;
using System;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Level 
{
    public class LevelDestroyingController : ControllerBase<LevelInfo>, IInitializable, IDisposable
    {
        public event Action LevelWasDestroyed;

        public LevelDestroyingController(LevelInfo levelInfo) : base(levelInfo) { }

        public void Initialize()
        {
            ControlledEntity.CurrentLevelView.OnUpdate += DestroyOldLevel;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentLevelView.OnUpdate -= DestroyOldLevel;
        }

        private void DestroyOldLevel(RxValue<LevelView> rxValue) 
        {
            if(rxValue.OldValue != null) 
            {
                UnityEngine.Object.Destroy(rxValue.OldValue.gameObject);
                LevelWasDestroyed?.Invoke();
            }
        }
    }
}