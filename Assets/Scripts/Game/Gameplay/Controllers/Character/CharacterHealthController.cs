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
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly CharacterRespawnService _characterRespawnService;

        public CharacterHealthController(CharacterHealthView controlledEntity, CharacterCharacteristicsRepository characterCharacteristicsRepository, CharacterRespawnService characterRespawnService) : base(controlledEntity)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _characterRespawnService = characterRespawnService;
        }

        public void Initialize()
        {
            _characterCharacteristicsRepository.CurrentHealth.OnUpdate += CurrentHealthOnUpdateHandler;
        }

        public void Dispose()
        {
            _characterCharacteristicsRepository.CurrentHealth.OnUpdate -= CurrentHealthOnUpdateHandler;
        }

        private void CurrentHealthOnUpdateHandler(RxValue<float> rxValue)
        {
            ControlledEntity.SetHealthPercentage(rxValue.NewValue / _characterCharacteristicsRepository.MaxHealth);

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