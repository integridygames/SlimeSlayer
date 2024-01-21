using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Components.AttackComponents
{
    public class FireBallAttackComponent : IEnemyAttackComponent
    {
        private readonly EnemyViewBase _enemyViewBase;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly ActiveProjectilesContainer _activeProjectilesContainer;
        private readonly CharacterView _characterView;

        public bool IsOnAttack { get; private set; }

        public FireBallAttackComponent(EnemyViewBase enemyViewBase, CharacterCharacteristicsRepository characterCharacteristicsRepository, BulletsPoolFactory bulletsPoolFactory,
            ActiveProjectilesContainer activeProjectilesContainer, CharacterView characterView)
        {
            _enemyViewBase = enemyViewBase;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeProjectilesContainer = activeProjectilesContainer;
            _characterView = characterView;
        }

        public bool ReadyToAttack()
        {
            return Vector3.Distance(_enemyViewBase.transform.position, _characterView.transform.position) <= 7f;
        }

        public void BeginAttack()
        {
            IsOnAttack = true;

            _enemyViewBase.SetAttackAnimation(true);

            var targetDirection = (_characterView.transform.position - _enemyViewBase.transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));

            _enemyViewBase.transform.rotation = lookRotation;

            var shootingPoint = _enemyViewBase.transform.position + _enemyViewBase.transform.forward;
            ShootBullet(shootingPoint, _enemyViewBase.transform.forward, 5, true);
        }

        private float _rotationProcess;

        public void ProcessAttack()
        {
            if (_rotationProcess < 1)
            {
                return;
            }
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyAttackCompleted += OnEnemyAttackCompleted;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyAttackCompleted -= OnEnemyAttackCompleted;
        }

        private void OnEnemyAttackCompleted()
        {
            _enemyViewBase.SetAttackAnimation(false);
            IsOnAttack = false;
        }

        public void ShootBullet(Vector3 shootingPoint, Vector3 direction,
            float damage, bool canBeMultiple = false)
        {
            if (_bulletsPoolFactory.GetElement(ProjectileType.FireBall) is FireBallView fireBallView)
            {
                fireBallView.transform.position = shootingPoint;

                fireBallView.Initialize(direction, damage, 5, canBeMultiple);
                fireBallView.Shoot();

                fireBallView.SetPlayerCollideAction(OnPlayerCollideHandler);

                _activeProjectilesContainer.AddProjectile(fireBallView);
            }
        }

        private void OnPlayerCollideHandler(FireBallView fireBallView)
        {
            _characterCharacteristicsRepository.RemoveHealth(fireBallView.Damage);
        }
    }
}