using Game.Gameplay.Models.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.Health
{
    public class CharacterRegenerationSystem : IUpdateSystem
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterRegenerationSystem(CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public void Update()
        {
            if (_characterCharacteristicsRepository.CurrentHealth.Value >=
                _characterCharacteristicsRepository.MaxHealth)
            {
                return;
            }

            _characterCharacteristicsRepository.AddHealth(_characterCharacteristicsRepository.MaxHealth *
                _characterCharacteristicsRepository.Regeneration / 100 * Time.deltaTime / 60);
        }
    }
}