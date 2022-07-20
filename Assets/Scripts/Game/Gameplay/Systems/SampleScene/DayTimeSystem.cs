using Game.Gameplay.Models.SampleScene;
using Game.Gameplay.Views.SampleScene;
using JetBrains.Annotations;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.SampleScene
{
    [UsedImplicitly]
    public class DayTimeSystem : IUpdateSystem
    {
        private const int DayTime = 24;
        private const int DegreesInCircle = 360;
        private const float MidnightSunAngle = 90f;

        private readonly DayTime _dayTime;

        private readonly TimeUiView _timeUiView;
        private readonly SunView _sunView;

        public DayTimeSystem(DayTime dayTime, TimeUiView timeUiView, SunView sunView)
        {
            _dayTime = dayTime;
            _timeUiView = timeUiView;
            _sunView = sunView;
        }
        
        public void Update()
        {
            _dayTime.CurrentTime += Time.deltaTime;

            var currentDayTime = _dayTime.CurrentTime % DayTime;
            _timeUiView.SetTime(currentDayTime);

            var sunAngle = DegreesInCircle * (currentDayTime / DayTime) - MidnightSunAngle;
            _sunView.SetAngle(sunAngle);
        }
    }
}