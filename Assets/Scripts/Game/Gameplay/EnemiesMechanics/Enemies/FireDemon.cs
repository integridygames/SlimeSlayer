using Game.Gameplay.EnemiesMechanics.Components.AttackComponents;
using Game.Gameplay.EnemiesMechanics.Components.DamageComponents;
using Game.Gameplay.EnemiesMechanics.Components.DeathComponents;
using Game.Gameplay.EnemiesMechanics.Components.MovementComponents;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using Zenject;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public class FireDemon : EnemyBase

    {
        private readonly CommonDeathComponent _commonDeathComponent;
        private readonly FireBallAttackComponent _fireBallAttackComponent;

        protected sealed override IEnemyMovementComponent EnemyMovementComponent { get; }
        protected override IEnemyDamageComponent EnemyDamageComponent { get; }
        protected override IEnemyAttackComponent EnemyAttackComponent => _fireBallAttackComponent;
        protected override IEnemyDeathComponent EnemyDeathComponent => _commonDeathComponent;

        public override float StartHealth => 60;
        public override float Speed => 6f;


        public FireDemon(FireDemonView fireDemonView, CharacterCharacteristicsRepository characterCharacteristicsRepository, CharacterView characterView,
            HealthBarsPoolFactory healthBarsPoolFactory, CameraContainerView cameraContainerView, CanvasView canvasView, DamageFxService damageFxService,
            BulletsPoolFactory bulletsPoolFactory, ActiveProjectilesContainer activeProjectilesContainer)
            : base(fireDemonView, characterCharacteristicsRepository, damageFxService, healthBarsPoolFactory, cameraContainerView, canvasView)
        {
            EnemyMovementComponent = new SmoothForwardMovementComponent(fireDemonView.NavMeshAgent, characterView);
            EnemyDamageComponent = new DummyDamageComponent();
            _fireBallAttackComponent = new FireBallAttackComponent(fireDemonView, characterCharacteristicsRepository, bulletsPoolFactory, activeProjectilesContainer, characterView);
            _commonDeathComponent = new CommonDeathComponent(fireDemonView);
        }

        protected override void OnStart()
        {
            _fireBallAttackComponent.Initialize();
            _commonDeathComponent.Initialize();
        }

        protected override void OnEnd()
        {
            _fireBallAttackComponent.Dispose();
            _commonDeathComponent.Dispose();
        }
    }
}