using Game.DataBase.Character;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.Models.Character
{
    public class CharacterCharacteristicsRepository
    {
        private const int StartReachPoint = 4;

        private readonly CharacterCharacteristics _characterCharacteristics;

        private int _nextLevelReachPoint;
        private int _currentExperience;

        private readonly RxField<float> _currentHealth = 0;
        public IReadonlyRxField<float> CurrentHealth => _currentHealth;

        public float MaxHealth { get; private set; }

        public float MovingSpeed { get; private set; }

        public float AttackRange { get; private set; }

        public float Regeneration { get; private set; }

        private readonly RxField<int> _currentLevel = 0;
        public IReadonlyRxField<int> CurrentLevel => _currentLevel;

        private readonly RxField<float> _currentLevelProgress = 0;
        public IReadonlyRxField<float> CurrentLevelProgress => _currentLevelProgress;

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

            _currentLevel.Value = 0;

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

            if (_currentExperience >= _nextLevelReachPoint)
            {
                LevelUp();
            }
        }

        public void ResetLevelBar()
        {
            _currentLevelProgress.Value = 0;
            _currentExperience = 0;
        }

        private void LevelUp()
        {
            _currentLevel.Value++;
            _nextLevelReachPoint = GetNextLevelReachPoint();
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