using System;
using Game.DataBase;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.UI;
using Game.Gameplay.WeaponMechanics;
using TegridyCore;
using TegridyUtils;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        private const float StartHealth = 100;

        private readonly EnemyViewBase _enemyViewBase;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly HealthBarsPoolFactory _healthBarsPoolFactory;
        private readonly CameraContainerView _cameraContainerView;
        private readonly CanvasView _canvasView;
        private readonly UiFxPoolFactory _uiFxPoolFactory;
        private readonly RxField<float> _health;
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

        public Vector3 Target
        {
            get => EnemyMovementComponent.Target;
            set => EnemyMovementComponent.Target = value;
        }

        public bool IsInCharacterRange { get; set; }

        protected EnemyBase(EnemyViewBase enemyViewBase, CharacterCharacteristicsRepository characterCharacteristicsRepository,
            HealthBarsPoolFactory healthBarsPoolFactory, CameraContainerView cameraContainerView, CanvasView canvasView, UiFxPoolFactory uiFxPoolFactory)
        {
            _enemyViewBase = enemyViewBase;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _healthBarsPoolFactory = healthBarsPoolFactory;
            _cameraContainerView = cameraContainerView;
            _canvasView = canvasView;
            _uiFxPoolFactory = uiFxPoolFactory;

            _health = StartHealth;
        }

        public void Initialize()
        {
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

            OnEnd();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnEnd()
        {
        }

        public void UpdateMovementData()
        {
            if (_isDead)
            {
                return;
            }

            EnemyMovementComponent.UpdateMovementData();

            UpdateHealthPosition();
        }

        public void UpdateMovement()
        {
            if (_isDead)
            {
                return;
            }

            EnemyMovementComponent.UpdateMovement();
        }

        public void Destroy()
        {
            Object.Destroy(_enemyViewBase.gameObject);
        }

        private void OnEnemyHitHandler(HitInfo hitInfo)
        {
            HandleDamage(hitInfo);
        }

        private void HandleDamage(HitInfo hitInfo)
        {
            if (_isDead)
            {
                return;
            }

            DoDamageFx(hitInfo);

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

        private void DoDamageFx(HitInfo hitInfo)
        {
            var damageFx = _uiFxPoolFactory.GetElement(0);

            var damageFxPosition = MathUtils.ToScreenPositionWithOffset(_enemyViewBase.transform.position, _cameraContainerView.Camera,
                0, 0);

            var randomStartPos = Random.insideUnitCircle * 60 * _canvasView.Canvas.scaleFactor;

            damageFxPosition.x += randomStartPos.x;
            damageFxPosition.y += randomStartPos.y;

            damageFx.transform.position = damageFxPosition;

            damageFx.StartFx(((int) hitInfo.Damage).ToString(),
                () => { _uiFxPoolFactory.RecycleElement(0, damageFx); });
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