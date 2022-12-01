using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.AttackComponents
{
    public class ImpulseAttackComponent : IEnemyAttackComponent
    {
        private const int Damage = 10;
        private const float MinDistanceToAttack = 5f;
        private const float EnemyAttackForce = 7.5f;
        private const float AttackTime = 1f;
        private const float AttackDuration = 4f;

        private readonly EnemyViewBase _enemyView;
        private readonly Rigidbody _enemyRigidbody;
        private readonly CharacterView _characterView;
        private readonly CharacterHealthData _characterHealthData;

        private float _timeFromAttackStart;
        private float _timeFromPreviousAttack;
        private bool _isInitialized;
        private bool _isOnAttack;

        public ImpulseAttackComponent(EnemyViewBase enemyView, Rigidbody enemyRigidbody, CharacterView characterView,
            CharacterHealthData characterHealthData)
        {
            _enemyView = enemyView;
            _enemyRigidbody = enemyRigidbody;
            _characterView = characterView;
            _characterHealthData = characterHealthData;
        }

        public bool ReadyToAttack()
        {
            if (_isInitialized == false)
            {
                _isInitialized = true;
                _timeFromPreviousAttack = Time.time;
            }

            return Time.time - _timeFromPreviousAttack >= AttackDuration &&
                   Vector3.Distance(_enemyRigidbody.transform.position, _characterView.transform.position) <= MinDistanceToAttack;
        }

        public void BeginAttack()
        {
            IsOnAttack = true;

            var direction = (_characterView.transform.position - _enemyRigidbody.transform.position).normalized;
            direction.y = 0;

            _enemyRigidbody.AddForce(direction * EnemyAttackForce, ForceMode.Impulse);
        }

        private void EnemyViewOnOnEnemyCollide(Collision collision)
        {
            if (collision.collider.TryGetComponent(out CharacterView _))
            {
                _characterHealthData.CurrentHealth.Value -= Damage;
            }
        }

        public void ProcessAttack()
        {
            if (Time.time - _timeFromAttackStart >= AttackTime)
            {
                IsOnAttack = false;
            }
        }

        public bool IsOnAttack
        {
            get => _isOnAttack;
            private set
            {
                if (value)
                {
                    _timeFromAttackStart = Time.time;
                    _enemyView.OnEnemyCollide += EnemyViewOnOnEnemyCollide;
                }
                else
                {
                    _timeFromPreviousAttack = Time.time;
                    _enemyView.OnEnemyCollide -= EnemyViewOnOnEnemyCollide;
                }

                _isOnAttack = value;
            }
        }
    }
}