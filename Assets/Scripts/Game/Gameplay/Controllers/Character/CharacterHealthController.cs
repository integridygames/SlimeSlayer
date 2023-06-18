using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Character;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class CharacterHealthController : ControllerBase<CharacterHealthView>, IInitializable, IDisposable
    {
        private readonly CharacterHealthData _characterHealthData;
        private readonly CharacterRespawnService _characterRespawnService;

        public CharacterHealthController(CharacterHealthView controlledEntity, CharacterHealthData characterHealthData, CharacterRespawnService characterRespawnService) : base(controlledEntity)
        {
            _characterHealthData = characterHealthData;
            _characterRespawnService = characterRespawnService;
        }

        public void Initialize()
        {
            _characterHealthData.CurrentHealth.OnUpdate += CurrentHealthOnUpdateHandler;
        }

        public void Dispose()
        {
            _characterHealthData.CurrentHealth.OnUpdate -= CurrentHealthOnUpdateHandler;
        }

        private void CurrentHealthOnUpdateHandler(RxValue<int> rxValue)
        {
            ControlledEntity.SetHealthPercentage((float) rxValue.NewValue / _characterHealthData.MaxHealth);

            if (rxValue.NewValue <= 0)
            {
                RespawnCharacter();
            }
        }

        private void RespawnCharacter()
        {
            _characterRespawnService.GoToSpawnPoint();
        }
    }
}