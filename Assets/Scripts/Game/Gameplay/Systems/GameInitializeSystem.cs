using Game.Gameplay.Models;
using Game.Services;
using JetBrains.Annotations;
using TegridyCore.Base;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Systems
{
    [UsedImplicitly]
    public class GameInitializeSystem : IPreInitializeSystem
    {
        private readonly StateMachine _stateMachine;
        private readonly SoundService _soundService;
        private readonly VibrationService _vibrationService;
        private readonly ApplicationData _applicationData;

        public GameInitializeSystem(StateMachine stateMachine, SoundService soundService,
            VibrationService vibrationService, ApplicationData applicationData)
        {
            _stateMachine = stateMachine;
            _soundService = soundService;
            _vibrationService = vibrationService;
            _applicationData = applicationData;
        }

        public void PreInitialize()
        {
            _vibrationService.IsEnabled = _applicationData.PlayerSettings.VibrationEnabled;
            _soundService.IsSoundsEnabled = _applicationData.PlayerSettings.SoundsEnabled.Value;
            _soundService.IsMusicEnabled = _applicationData.PlayerSettings.MusicEnabled.Value;
            
            _stateMachine.Start();
        }
    }
}