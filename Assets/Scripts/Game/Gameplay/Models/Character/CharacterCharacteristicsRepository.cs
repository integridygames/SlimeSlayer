using Game.DataBase.Abilities;
using Game.DataBase.Character;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Character;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterCharacteristicsRepository
    {
        private const int StartReachPoint = 4;

        private readonly CharacterCharacteristics _characterCharacteristics;
        private readonly CharacterView _characterView;
        private readonly AbilityTmpCharacteristics _abilityTmpCharacteristics;
        private readonly AbilitiesRepository _abilitiesRepository;
        private readonly DamageFxService _damageFxService;

        private int _nextLevelReachPoint;
        private int _currentExperience;

        private readonly RxField<float> _currentHealth = 0;
        public IReadonlyRxField<float> CurrentHealth => _currentHealth;

        public float AttackRange { get; private set; }

        private readonly RxField<int> _currentLevel = 1;

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

        public CharacterCharacteristicsRepository(CharacterCharacteristics characterCharacteristics, CharacterView characterView,
            AbilityTmpCharacteristics abilityTmpCharacteristics, AbilitiesRepository abilitiesRepository, DamageFxService damageFxService)
        {
            _characterCharacteristics = characterCharacteristics;
            _characterView = characterView;
            _abilityTmpCharacteristics = abilityTmpCharacteristics;
            _abilitiesRepository = abilitiesRepository;
            _damageFxService = damageFxService;
        }

        public void UpdateCharacteristics()
        {
            _maxHealth = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.MaxHealth);
            _currentHealth.Value = _maxHealth;

            MovingSpeed = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Speed);
            AttackRange = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.AttackRange);
            _regeneration = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.Regeneration);

            _healthSteal = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.HealthSteal);

            _nextLevelReachPoint = GetNextLevelReachPoint(1);
            _currentLevel.Value = 1;
            ResetLevelBar();

            _abilityTmpCharacteristics.Clear();
            _abilitiesRepository.Clear();
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

        public void RemoveHealth(float value)
        {
            _currentHealth.Value -= value;
            _damageFxService.DoDamageFx((int) value, _characterView.transform.position, 10);
        }

        public void AddExperience()
        {
            _currentExperience++;

            _currentLevelProgress.Value = (float) _currentExperience / _nextLevelReachPoint;
        }

        public void LevelUp()
        {
            _currentLevel.Value++;
            _nextLevelReachPoint = GetNextLevelReachPoint(_currentLevel.Value);

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
            _currentExperience = 0;
            _currentLevelProgress.Value = 0;
        }

        private static int GetNextLevelReachPoint(int level)
        {
            if (level == 1)
            {
                return StartReachPoint;
            }

            return (int) (StartReachPoint * Mathf.Pow(2, Mathf.Sqrt(level)));
        }
    }
}