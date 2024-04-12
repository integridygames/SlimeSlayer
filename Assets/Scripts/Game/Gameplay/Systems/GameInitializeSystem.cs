using Game.Gameplay.Models;
using Game.Services;
using JetBrains.Annotations;
using TegridyCore.Base;
using TegridyCore.FiniteStateMachine;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    [UsedImplicitly]
    public class GameInitializeSystem : IPreInitializeSystem
    {
        private readonly StateMachine _stateMachine;
        private readonly SoundService _soundService;
        private readonly ApplicationData _applicationData;

        public GameInitializeSystem(StateMachine stateMachine, SoundService soundService,
            ApplicationData applicationData)
        {
            _stateMachine = stateMachine;
            _soundService = soundService;
            _applicationData = applicationData;
        }

        public void PreInitialize()
        {
            _soundService.Init(_applicationData.PlayerSettings);

            _stateMachine.Start();
        }
    }
}