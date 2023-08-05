using Game.DataBase.Abilities;
using Game.DataBase.Character;
using Game.Gameplay.Models.Abilities;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterCharacteristicsRepository
    {
        private const int StartReachPoint = 4;

        private readonly CharacterCharacteristics _characterCharacteristics;
        private readonly AbilityTmpCharacteristics _abilityTmpCharacteristics;
        private readonly AbilitiesRepository _abilitiesRepository;

        private int _nextLevelReachPoint;
        private int _currentExperience;

        private readonly RxField<float> _currentHealth = 0;
        public IReadonlyRxField<float> CurrentHealth => _currentHealth;

        public float AttackRange { get; private set; }

        private readonly RxField<int> _currentLevel = 0;

        public IReadonlyRxField<int> CurrentLevel => _currentLevel;

        private readonly RxField<float> _currentLevelProgress = 0;

        public IReadonlyRxField<float> CurrentLevelProgress => _currentLevelProgress;

        private float _healthSteal;

        private float _regeneration;

        public float Regeneration
        {
            get
            {
                TryGetAbilityCharacteristic(AbilityCharacteristicType.AdditionalHealthRegeneration,
                    out float additionalRegeneration);

                return _regeneration + additionalRegeneration;
            }
        }

        private float _maxHealth;

        public float MaxHealth
        {
            get
            {
                TryGetAbilityCharacteristic(AbilityCharacteristicType.AdditionalHealth, out float additionalHealth);
                return _maxHealth + additionalHealth;
            }
        }

        private float _movingSpeed;

        public float MovingSpeed
        {
            get
            {
                TryGetAbilityCharacteristic(AbilityCharacteristicType.AdditionalMovementSpeed,
                    out float additionalMovementSpeed);

                return _movingSpeed + additionalMovementSpeed;
            }
            private set => _movingSpeed = value;
        }

        public bool ReadyForLevelUp => _currentExperience >= _nextLevelReachPoint;

        public CharacterCharacteristicsRepository(CharacterCharacteristics characterCharacteristics,
            AbilityTmpCharacteristics abilityTmpCharacteristics, AbilitiesRepository abilitiesRepository)
        {
            _characterCharacteristics = characterCharacteristics;
            _abilityTmpCharacteristics = abilityTmpCharacteristics;
            _abilitiesRepository = abilitiesRepository;
        }

        public void UpdateCharacteristics()
        {
            _currentHealth.Value = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.MaxHealth);
            _maxHealth = CurrentHealth.Value;

            MovingSpeed = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Speed);
            AttackRange = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.AttackRange);
            _regeneration = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Regeneration);

            _healthSteal = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.HealthSteal);

            _currentLevel.Value = 0;

            _abilityTmpCharacteristics.Clear();
            _abilitiesRepository.Clear();

            LevelUp();
            ResetLevelBar();
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

        public void AddExperience()
        {
            _currentExperience++;

            _currentLevelProgress.Value = (float) _currentExperience / _nextLevelReachPoint;
        }

        public void LevelUp()
        {
            _currentLevel.Value++;
            _nextLevelReachPoint = GetNextLevelReachPoint();

            ResetLevelBar();
        }

        public void LevelUpAbility(AbilityRecord abilityRecord, int level)
        {
            var abilityLevelRecord = abilityRecord.GetInfoForLevel(level);
            foreach (var abilityCharacteristic in abilityLevelRecord._abilityCharacteristics)
            {
                var characteristicType = abilityCharacteristic._abilityCharacteristicType;
                _abilityTmpCharacteristics.SetCharacteristic(characteristicType, abilityCharacteristic._value);
            }
        }

        public bool TryGetAbilityCharacteristic(AbilityCharacteristicType characteristicType, out float value)
        {
            return _abilityTmpCharacteristics.TryGetCharacteristic(characteristicType, out value);
        }

        public bool TryGetAbilityCharacteristic(AbilityCharacteristicType characteristicType, out int value)
        {
            var result = _abilityTmpCharacteristics.TryGetCharacteristic(characteristicType, out var floatValue);
            value = (int) floatValue;

            return result;
        }

        private void ResetLevelBar()
        {
            _currentLevelProgress.Value = 0;
            _currentExperience = 0;
        }

        private int GetNextLevelReachPoint()
        {
            if (_currentLevel.Value == 1)
            {
                return StartReachPoint;
            }

            return (int) (StartReachPoint * Mathf.Pow(2, Mathf.Sqrt(_currentLevel.Value)));
        }
    }
}