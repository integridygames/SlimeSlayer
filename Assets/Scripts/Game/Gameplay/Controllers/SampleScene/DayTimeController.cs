using System;
using Game.Gameplay.Models.SampleScene;
using Game.Gameplay.Views.SampleScene;
using JetBrains.Annotations;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.SampleScene
{
    [UsedImplicitly]
    public class DayTimeController : ControllerBase<ChangeTimeView>, IInitializable, IDisposable
    {
        private const float HalfDayTime = 12f;
        
        private readonly DayTime _dayTime;

        public DayTimeController(ChangeTimeView changeTimeView, DayTime dayTime) : base(changeTimeView)
        {
            _dayTime = dayTime;
        }

        public void Initialize()
        {
            ControlledEntity.ChangeTimeButton.OnReleased += OnChangeTimeHandler;
        }

        public void Dispose()
        {
            ControlledEntity.ChangeTimeButton.OnReleased -= OnChangeTimeHandler;
        }
        
        private void OnChangeTimeHandler()
        {
            _dayTime.CurrentTime += HalfDayTime;
        }
    }
}