using System;
using Game.DataBase;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.UI;
using Game.Gameplay.WeaponMechanics;
using TegridyCore;
using TegridyUtils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        private readonly EnemyViewBase _enemyViewBase;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly DamageFxService _damageFxService;
        private readonly HealthBarsPoolFactory _healthBarsPoolFactory;
        private readonly CameraContainerView _cameraContainerView;
        private readonly CanvasView _canvasView;
        private readonly RxField<float> _health = new();
        private bool _isDead;

        private EnemyHealthView _enemyHealthView;

        protected abstract IEnemyMovementComponent EnemyMovementComponent { get; }
        protected abstract IEnemyDamageComponent EnemyDamageComponent { get; }
        protected abstract IEnemyAttackComponent EnemyAttackComponent { get; }
        protected abstract IEnemyDeathComponent EnemyDeathComponent { get; }

        public event Action<EnemyBase> OnEnemyDied;

        public bool IsOnAttack => EnemyAttackComponent.IsOnAttack;
        public bool ReadyToAttack() => EnemyAttackComponent.ReadyToAttack();
        public void BeginAttack() => EnemyAttackComponent.BeginAttack();
        public void ProcessAttack() => EnemyAttackComponent.ProcessAttack();

        public Vector3 Position => EnemyMovementComponent.Position;

        public bool IsInCharacterRange { get; set; }

        public bool IsDead => _isDead;

        public abstract float StartHealth { get; }

        public abstract float Speed { get; }

        protected EnemyBase(EnemyViewBase enemyViewBase, CharacterCharacteristicsRepository characterCharacteristicsRepository, DamageFxService damageFxService,
            HealthBarsPoolFactory healthBarsPoolFactory, CameraContainerView cameraContainerView, CanvasView canvasView)
        {
            _enemyViewBase = enemyViewBase;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _damageFxService = damageFxService;
            _healthBarsPoolFactory = healthBarsPoolFactory;
            _cameraContainerView = cameraContainerView;
            _canvasView = canvasView;
        }

        public void Initialize()
        {
            _health.Value = StartHealth;

            _enemyViewBase.OnEnemyHit += OnEnemyHitHandler;
            EnemyDeathComponent.OnDied += OnEnemyDiedHandler;

            _enemyHealthView = _healthBarsPoolFactory.GetElement(HealthBarType.Red);
            _enemyHealthView.gameObject.SetActive(false);

            OnStart();

            _enemyHealthView.SetHealthPercentage(_health.Value / StartHealth);
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyHit -= OnEnemyHitHandler;
            EnemyDeathComponent.OnDied -= OnEnemyDiedHandler;

            _healthBarsPoolFactory.RecycleElement(HealthBarType.Red, _enemyHealthView);

            Object.Destroy(_enemyViewBase.gameObject);

            OnEnd();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnEnd()
        {
        }

        public void UpdateMovement()
        {
            UpdateHealthPosition();

            if (IsDead)
            {
                return;
            }

            EnemyMovementComponent.UpdateMovement(Speed, IsOnAttack);
        }

        private void OnEnemyHitHandler(HitInfo hitInfo)
        {
            HandleDamage(hitInfo);
        }

        private void HandleDamage(HitInfo hitInfo)
        {
            if (IsDead)
            {
                return;
            }

            _damageFxService.DoDamageFx((int) hitInfo.Damage, hitInfo.HitPosition, 70);

            _health.Value -= hitInfo.Damage;

            if (_health.Value <= 0)
            {
                _isDead = true;
                EnemyDeathComponent.BeginDie();
                _enemyHealthView.gameObject.SetActive(false);
                return;
            }

            EnemyDamageComponent.Hit(hitInfo);

            _enemyHealthView.gameObject.SetActive(true);
            _enemyHealthView.SetHealthPercentage(_health.Value / StartHealth);

            _characterCharacteristicsRepository.HandleHealthStealing();
        }

        private void OnEnemyDiedHandler()
        {
            OnEnemyDied?.Invoke(this);
        }

        private void UpdateHealthPosition()
        {
            var screenPosition = MathUtils.ToScreenPositionWithOffset(_enemyViewBase.transform.position, _cameraContainerView.Camera,
                _enemyHealthView.VerticalOffset, _canvasView.Canvas.scaleFactor);

            _enemyHealthView.transform.position = screenPosition;
        }
    }
}