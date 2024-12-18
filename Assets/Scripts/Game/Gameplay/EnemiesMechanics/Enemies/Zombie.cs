﻿using Game.Gameplay.EnemiesMechanics.Components.AttackComponents;
using Game.Gameplay.EnemiesMechanics.Components.DamageComponents;
using Game.Gameplay.EnemiesMechanics.Components.DeathComponents;
using Game.Gameplay.EnemiesMechanics.Components.MovementComponents;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public class Zombie : EnemyBase
    {
        private readonly CloseAttackComponent _closeAttackComponent;
        private readonly CommonDeathComponent _commonDeathComponent;

        protected override IEnemyMovementComponent EnemyMovementComponent { get; }
        protected override IEnemyDamageComponent EnemyDamageComponent { get; }
        protected override IEnemyAttackComponent EnemyAttackComponent => _closeAttackComponent;
        protected override IEnemyDeathComponent EnemyDeathComponent => _commonDeathComponent;

        public override float StartHealth => 40;
        public override float Speed => 1.5f;

        public Zombie(ZombieView zombieView, CharacterCharacteristicsRepository characterCharacteristicsRepository, CharacterView characterView,
            HealthBarsPoolFactory healthBarsPoolFactory, CameraContainerView cameraContainerView, CanvasView canvasView, DamageFxService damageFxService)
            : base(zombieView, characterCharacteristicsRepository, damageFxService, healthBarsPoolFactory, cameraContainerView, canvasView)
        {
            EnemyMovementComponent = new SmoothForwardMovementComponent(zombieView.NavMeshAgent, characterView);
            EnemyDamageComponent = new DummyDamageComponent();
            _closeAttackComponent = new CloseAttackComponent(zombieView, characterView.transform,
                zombieView.transform, characterCharacteristicsRepository, 1);
            _commonDeathComponent = new CommonDeathComponent(zombieView);
        }

        protected override void OnStart()
        {
            _closeAttackComponent.Initialize();
            _commonDeathComponent.Initialize();
        }

        protected override void OnEnd()
        {
            _closeAttackComponent.Dispose();
            _commonDeathComponent.Dispose();
        }
    }
}