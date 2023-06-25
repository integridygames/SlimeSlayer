﻿using Game.DataBase.Character;
using TegridyCore;

namespace Game.Gameplay.Models.Character
{
    public class CharacterCharacteristicsRepository
    {
        private readonly CharacterCharacteristics _characterCharacteristics;

        private readonly RxField<float> _currentHealth = 0;
        public IReadonlyRxField<float> CurrentHealth => _currentHealth;

        public float MaxHealth { get; private set; }

        public float MovingSpeed { get; private set; }

        public float AttackRange { get; private set; }

        public float Regeneration { get; private set; }

        private float _healthSteal;

        public CharacterCharacteristicsRepository(CharacterCharacteristics characterCharacteristics)
        {
            _characterCharacteristics = characterCharacteristics;
        }

        public void UpdateCharacteristics()
        {
            _currentHealth.Value = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.MaxHealth);
            MaxHealth = CurrentHealth.Value;

            MovingSpeed = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Speed);
            AttackRange = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.AttackRange);
            Regeneration = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Regeneration);

            _healthSteal = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.HealthSteal);
        }

        public void HandleHealthStealing()
        {
            AddHealth(_healthSteal);
        }

        public void AddHealth(float value)
        {
            if (value >= MaxHealth)
            {
                _currentHealth.Value = MaxHealth;
                return;
            }

            _currentHealth.Value += value;
        }
    }
}