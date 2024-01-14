using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class CharacterHealthController : ControllerBase<CharacterHealthView>, IInitializable, IDisposable
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterHealthController(CharacterHealthView controlledEntity, CharacterCharacteristicsRepository characterCharacteristicsRepository) : base(controlledEntity)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
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
        }
    }
}