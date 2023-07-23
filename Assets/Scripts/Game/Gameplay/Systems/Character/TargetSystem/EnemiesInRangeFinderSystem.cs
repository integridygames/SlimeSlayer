using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Character.TargetSystem
{
    public class EnemiesInRangeFinderSystem : IUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterView _characterView;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public EnemiesInRangeFinderSystem(ActiveEnemiesContainer activeEnemiesContainer,
            CharacterView characterView, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterView = characterView;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public void Update()
        {
            foreach (var enemy in _activeEnemiesContainer.ActiveEnemies)
            {
                enemy.IsInCharacterRange = EnemyIsInRangeOfCharacter(enemy);
            }
        }

        private bool EnemyIsInRangeOfCharacter(EnemyBase enemy)
        {
            var distance = Vector3.Distance(_characterView.transform.position, enemy.Position);
            return distance <= _characterCharacteristicsRepository.AttackRange;
        }
    }
}